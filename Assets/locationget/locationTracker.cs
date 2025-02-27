using UnityEngine;
using TMPro;  // TextMeshProを使用するために追加

public class LocationTracker : MonoBehaviour
{
    // TextMeshProUGUIを使う場合
    public TextMeshProUGUI locationText;

    void Start()
    {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }
        else
        {
            locationText.text = "GPSが無効です。";
        }
    }

    void Update()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;

            locationText.text = "緯度: " + latitude.ToString() + "\n経度: " + longitude.ToString();
        }
        else
        {
            locationText.text = "位置情報取得中...";
        }
    }

    void OnApplicationQuit()
    {
        Input.location.Stop();
    }
}
