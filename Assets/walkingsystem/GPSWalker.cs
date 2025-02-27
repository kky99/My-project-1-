using UnityEngine;
using System.Collections;

public class GPSWalker : MonoBehaviour
{
    private Vector2 lastPosition;
    private float lastTime;

    IEnumerator Start()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("GPSが無効です");
            yield break;
        }

        Input.location.Start();
        yield return new WaitForSeconds(2);

        if (Input.location.status != LocationServiceStatus.Running)
        {
            Debug.Log("GPSが取得できません");
            yield break;
        }

        lastPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        lastTime = Time.time;

        StartCoroutine(CheckSpeed());
    }

    IEnumerator CheckSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(2); // 2秒ごとにチェック

            Vector2 currentPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            float distance = Vector2.Distance(lastPosition, currentPosition);
            float timeElapsed = Time.time - lastTime;

            float speed = (distance / timeElapsed) * 3600; // km/h に換算

            Debug.Log("速度: " + speed + " km/h");

            if (speed > 10) // 10km/h以上ならバス・タクシー判定
            {
                Debug.Log("バス・タクシー使用の可能性！");
                // ペナルティ処理を書く（ポイント減少・歩行カウント無効 など）
            }

            lastPosition = currentPosition;
            lastTime = Time.time;
        }
    }
}

