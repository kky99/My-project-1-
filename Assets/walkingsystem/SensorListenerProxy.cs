using UnityEngine;

public class SensorListenerProxy : AndroidJavaProxy
{
    private StepCounterAndroid stepCounterAndroid;

    // コンストラクタで StepCounterAndroid インスタンスを受け取る
    public SensorListenerProxy(StepCounterAndroid stepCounter)
        : base("android.hardware.SensorEventListener")
    {
        stepCounterAndroid = stepCounter;
    }

    // センサーイベント（歩数センサーや加速度センサー）を処理
    public void onSensorChanged(AndroidJavaObject sensorEvent)
    {
        try
        {
            // センサータイプを取得
            int sensorType = sensorEvent.Call<int>("getType");

            // 歩数センサーの場合
            if (sensorType == 19) // TYPE_STEP_COUNTER
            {
                // 歩数を取得（センサーから受け取った値）
                float stepCount = sensorEvent.Call<float>("values", 0);
                stepCounterAndroid.OnStepSensorChanged(stepCount); // 歩数更新
            }
            // 加速度センサーの場合
            else if (sensorType == 1) // TYPE_ACCELEROMETER
            {
                // 加速度値を取得
                float x = sensorEvent.Call<float>("getX");
                float y = sensorEvent.Call<float>("getY");
                float z = sensorEvent.Call<float>("getZ");

                // 加速度のベクトルを作成
                Vector3 acceleration = new Vector3(x, y, z);
                stepCounterAndroid.OnAccelerometerSensorChanged(acceleration); // 加速度更新
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("センサーイベントエラー: " + e.Message);
        }
    }

    // 精度変更時の処理（必要なら追加）
    public void onAccuracyChanged(AndroidJavaObject sensor, int accuracy)
    {
        // 精度変更時の処理（未実装のままでも問題なし）
    }
}
