using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StaticMapController : MonoBehaviour
{
    private const string googleMapsApiKey = "AIzaSyBWL2_w5vkL3OCYPB9wZi6ttFVDO1rX3D4 "; // APIキー  
    private const float defaultLatitude = 43.19191f; // 小樽商科大学の緯度
    private const float defaultLongitude = 140.98765f; // 小樽商科大学の経度
    private const int zoomLevel = 15;
    private const int mapSize = 640;

    public CheckpointManager checkpointManager; // チェックポイントマネージャーの参照
    public float zoomSpeed = 0.1f; // ズームの速度
    private float minZoom = 5f;    // 最小ズームレベル
    private float maxZoom = 20f;   // 最大ズームレベル
    private Camera mainCamera;     // メインカメラの参照

    // チェックポイントのデータ用クラス
    public class CheckpointData
    {
        public string name;
        public double latitude;
        public double longitude;
        public double checkpointDatalist;
    }

    void Start()
    {
        mainCamera = Camera.main;  // メインカメラを取得
        StartCoroutine(GetLocationAndUpdateMap());
    }

    void Update()
    {
        if (Input.touchCount == 2)  // 2本指でのタッチ
        {
            Debug.Log("2本指タッチ検出");
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float previousDistance = (touch1.position - touch2.position).magnitude;
            float currentDistance = (touch1.position - touch2.position).magnitude;

            // ズームイン・ズームアウト処理
            if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float deltaDistance = previousDistance - currentDistance;

                float zoom = mainCamera.orthographicSize + deltaDistance * zoomSpeed;
                mainCamera.orthographicSize = Mathf.Clamp(zoom, minZoom, maxZoom);  // ズーム範囲を制限
            }

            Debug.Log($"Touch1: {touch1.position}, Touch2: {touch2.position}");
        }
        else
        {
            Debug.Log("タッチイベントが2本ではありません");
        }
    }

    public void UpdateMap(float latitude, float longitude, List<CheckpointData> checkpointDataList)
    {
        StartCoroutine(GetStaticMap(latitude, longitude));
    }

    IEnumerator GetLocationAndUpdateMap()
    {
        float latitude = defaultLatitude;
        float longitude = defaultLongitude;

        Debug.Log("座標取得開始");

#if UNITY_EDITOR
        Debug.Log("エディター環境のため仮の座標を使用");
#else
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int maxWait = 10;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
                Debug.Log($"取得した座標: {latitude}, {longitude}");
            }
            else
            {
                Debug.LogWarning("GPSの取得に失敗。仮の座標を使用。");
            }
            Input.location.Stop();
        }
        else
        {
            Debug.LogWarning("GPSが無効。仮の座標を使用。");
        }
#endif

        yield return StartCoroutine(GetStaticMap(latitude, longitude));
    }

    IEnumerator GetStaticMap(float latitude, float longitude)
    {
        string style = "&style=feature:all|element:labels|visibility:off"
                     + "&style=feature:road|element:geometry|color:0xffffff"
                     + "&style=feature:water|element:geometry|color:0xadd8e6"
                     + "&style=feature:landscape|element:geometry|color:0xeeeeee";

        string markers = $"&markers=color:blue%7Clabel:U%7C{latitude},{longitude}";

        if (checkpointManager != null)
        {
            foreach (var checkpoint in checkpointManager.checkpoints)
            {
                string label = "X";
                if (!string.IsNullOrEmpty(checkpoint.checkpointName))
                {
                    label = checkpoint.checkpointName[0].ToString();
                }
                markers += $"&markers=color:red%7Clabel:{label}%7C{checkpoint.latitude},{checkpoint.longitude}";
            }
        }

        string url = $"https://maps.googleapis.com/maps/api/staticmap?key={googleMapsApiKey}&zoom={zoomLevel}&size={mapSize}x{mapSize}&scale=1&maptype=terrain&center={latitude},{longitude}{style}{markers}";

        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);

        req.timeout = 10;

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)req.downloadHandler).texture;

            if (texture == null)
            {
                Debug.LogError("テクスチャがnullです！");
                yield break;
            }

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRendererが見つかりません");
                yield break;
            }

            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.position = Vector3.zero;
            spriteRenderer.transform.localScale = new Vector3(1, 1, 1);
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
        else
        {
            Debug.LogError($"マップの取得に失敗: {req.error}, ステータスコード: {req.responseCode}");
        }
    }
}
