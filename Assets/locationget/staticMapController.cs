using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MapDisplay : MonoBehaviour
{
    private const string googleMapsApiKey = "AIzaSyBWL2_w5vkL3OCYPB9wZi6ttFVDO1rX3D4"; // APIキー（本番では.envなどで管理すべき）

    [Header("手動で座標を設定（GPSが無効の場合に使用）")]
    public float manualLatitude = 43.1896f;  // 小樽商科大学の緯度
    public float manualLongitude = 141.002f; // 小樽商科大学の経度

    private float latitude;
    private float longitude;

    void Start()
    {
        StartCoroutine(GetLocationAndUpdateMap());
    }

    IEnumerator GetLocationAndUpdateMap()
    {
        latitude = manualLatitude;  // デフォルトは手動入力の座標
        longitude = manualLongitude;

        Debug.Log("座標取得開始");

#if UNITY_EDITOR
        Debug.Log("エディター環境のためInspectorの座標を使用");
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
                Debug.Log($"GPSの座標取得成功: {latitude}, {longitude}");
            }
            else
            {
                Debug.LogWarning("GPSの取得に失敗。Inspectorの座標を使用");
            }
            Input.location.Stop();
        }
        else
        {
            Debug.LogWarning("GPSが無効。Inspectorの座標を使用");
        }
#endif

        yield return StartCoroutine(GetStaticMap(latitude, longitude));
    }

    IEnumerator GetStaticMap(float latitude, float longitude)
    {
        string query = $"&center={latitude},{longitude}&markers={latitude},{longitude}";
        string url = $"https://maps.googleapis.com/maps/api/staticmap?key={googleMapsApiKey}&zoom=15&size=640x640&scale=2&maptype=terrain" + query;

        Debug.Log("マップ取得URL: " + url);
        UnityWebRequest req = UnityWebRequestTexture.GetTexture(url);
        req.timeout = 10;

        Debug.Log("リクエスト送信中...");
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("マップ画像の取得に成功");
            Texture2D texture = ((DownloadHandlerTexture)req.downloadHandler).texture;

            if (texture == null)
            {
                Debug.LogError("テクスチャがnullです！");
                yield break;
            }

            texture.Apply();
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRendererが見つかりません");
                yield break;
            }

            spriteRenderer.sprite = sprite;
            spriteRenderer.transform.position = Vector3.zero;
            spriteRenderer.transform.localScale = new Vector3(5, 5, 1);
            spriteRenderer.color = new Color(1, 1, 1, 1);

            Debug.Log("スプライトが適用されました！");
        }
        else
        {
            Debug.LogError($"マップの取得に失敗: {req.error}, ステータスコード: {req.responseCode}");
        }
    }
}
