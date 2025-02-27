using UnityEngine;

public class StepChecker : MonoBehaviour
{
    private int stepCount;
    private Vector2 lastPosition;
    
    void Start()
    {
        lastPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        Input.location.Start();
    }

    void Update()
    {
        if (Input.acceleration.magnitude > 1.2f) // 加速度の変化で歩行を検知
        {
            stepCount++;
        }

        Vector2 currentPosition = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
        float distance = Vector2.Distance(lastPosition, currentPosition);

        if (distance > 50 && stepCount < 10) // 50m以上動いたのに歩数が10以下ならバス・タクシー判定
        {
            Debug.Log("バス・タクシー使用の可能性！");
        }

        lastPosition = currentPosition;
    }
}
