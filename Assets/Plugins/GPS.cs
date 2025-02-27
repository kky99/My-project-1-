using System.Collections; 
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{
    public Text locationText;

    void Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            locationText.text = "位置情報サービスが無効です。";
            return;
        }

        Input.location.Start();
        int maxWait = 10;

        // IEnumerator を返すコルーチンの呼び出し
        StartCoroutine(GetLocation(maxWait));
    }

    private IEnumerator GetLocation(int maxWait)
    {
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            locationText.text = "位置情報の初期化に失敗しました。";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            locationText.text = "位置情報の取得に失敗しました。";
        }
        else
        {
            float latitude = Input.location.lastData.latitude;
            float longitude = Input.location.lastData.longitude;
            locationText.text = "Latitude: " + latitude + "\nLongitude: " + longitude;
        }
    }

    private void OnApplicationQuit()
    {
        Input.location.Stop();
    }
}
