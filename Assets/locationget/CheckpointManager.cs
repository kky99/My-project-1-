using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckpointManager : MonoBehaviour
{
    [System.Serializable]
    public class Checkpoint
    {
        public string checkpointName;
        public double latitude;
        public double longitude;
        [HideInInspector]
        public bool reached = false;
    }

    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    public float thresholdDistance = 10f;
    public float updateInterval = 5f;

    public StaticMapController mapController; // StaticMapController を参照

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("位置情報サービスが無効です");
            yield break;
        }

        Input.location.Start();
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1 || Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("位置情報取得に失敗");
            yield break;
        }

        // チェックポイントの位置情報を渡してマップを更新
        List<StaticMapController.CheckpointData> checkpointDataList = new List<StaticMapController.CheckpointData>();
        foreach (var checkpoint in checkpoints)
        {
            if (!string.IsNullOrEmpty(checkpoint.checkpointName)) // 名前が空でないことを確認
            {
                checkpointDataList.Add(new StaticMapController.CheckpointData
                {
                    latitude = checkpoint.latitude,
                    longitude = checkpoint.longitude,
                    name = checkpoint.checkpointName
                });
            }
            else
            {
                Debug.LogWarning($"チェックポイントに名前が設定されていません: 緯度 {checkpoint.latitude}, 経度 {checkpoint.longitude}");
            }
        }

        while (true)
        {
            var loc = Input.location.lastData;
            Debug.Log($"現在位置: 緯度 {loc.latitude}, 経度 {loc.longitude}");

            foreach (var checkpoint in checkpoints)
            {
                if (!checkpoint.reached && IsWithinDistance(loc.latitude, loc.longitude, checkpoint.latitude, checkpoint.longitude, thresholdDistance))
                {
                    checkpoint.reached = true;
                    Debug.Log($"チェックポイント [{checkpoint.checkpointName}] に到達！");
                }
            }

            // マップ更新（現在位置 & チェックポイントを反映）
            if (mapController != null)
            {
                mapController.UpdateMap((float)loc.latitude, (float)loc.longitude, checkpointDataList);
            }

            yield return new WaitForSeconds(updateInterval);
        }
    }

    // 2点間の距離が指定の閾値以下かを判定するメソッド
    bool IsWithinDistance(double lat1, double lon1, double lat2, double lon2, float threshold)
    {
        double R = 6371000; // 地球の半径 (メートル)
        double dLat = Mathf.Deg2Rad * (lat2 - lat1);
        double dLon = Mathf.Deg2Rad * (lon2 - lon1);

        double a = Mathf.Sin((float)dLat / 2) * Mathf.Sin((float)dLat / 2) +
                   Mathf.Cos(Mathf.Deg2Rad * (float)lat1) * Mathf.Cos(Mathf.Deg2Rad * (float)lat2) *
                   Mathf.Sin((float)dLon / 2) * Mathf.Sin((float)dLon / 2);

        double c = 2 * Mathf.Atan2(Mathf.Sqrt((float)a), Mathf.Sqrt((float)(1 - a)));
        double distance = R * c;
        return distance <= threshold;
    }
} 