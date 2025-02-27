using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowGPS : MonoBehaviour
{
    public GameObject LatitudeText = null; // 緯度を表示するための文字
    public GameObject LongitudeText = null; // 経度を表示するための文字

    void Start()
    {
        Input.location.Start(); // GPS機能の利用開始
    }

    void Update()
    {
        if (LatitudeText == null || LongitudeText == null)
        {
            Debug.LogError("LatitudeText または LongitudeText がアタッチされていません！");
            return;
        }

        Text latitude_component = LatitudeText.GetComponent<Text>();
        Text longitude_component = LongitudeText.GetComponent<Text>();

        if (latitude_component == null || longitude_component == null)
        {
            Debug.LogError("Textコンポーネントが見つかりません！TextMeshProを使っていませんか？");
            return;
        }

        latitude_component.text = Input.location.status == LocationServiceStatus.Running
            ? Input.location.lastData.latitude.ToString()
            : "緯度取得失敗";

        longitude_component.text = Input.location.status == LocationServiceStatus.Running
            ? Input.location.lastData.longitude.ToString()
            : "経度取得失敗";
        
    }
}
