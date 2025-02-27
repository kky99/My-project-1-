#if UNITY_ANDROID
using UnityEngine;
using System;

public class StepCounterAndroid : StepCounterBase
{
    private AndroidJavaObject sensorManager;
    private AndroidJavaObject stepSensor;
    private AndroidJavaObject accelerometerSensor;
    private int initialStepCount = -1;

    private float prevYAcceleration = 0.0f; // 前回の加速度値
    private float threshold = 1.2f; // 歩行判定のしきい値

    private SensorListenerProxy sensorListener;

    // 初期化処理（StepCounterBaseのInitializeをオーバーライド）
    public override void Initialize()
    {
        base.Initialize();  // 親クラスの初期化を呼び出す

        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            // センサー関連
            sensorManager = activity.Call<AndroidJavaObject>("getSystemService", "sensor");
            stepSensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 19); // TYPE_STEP_COUNTER
            accelerometerSensor = sensorManager.Call<AndroidJavaObject>("getDefaultSensor", 1); // TYPE_ACCELEROMETER

            // 歩数センサーが存在すれば登録
            if (stepSensor != null)
            {
                sensorListener = new SensorListenerProxy(this);
                sensorManager.Call("registerListener", sensorListener, stepSensor, 3); // 歩数センサー
            }
            else
            {
                Debug.LogWarning("歩数センサーがないため、加速度センサーを使用します。");
            }

            // 加速度センサーが存在すれば登録
            if (accelerometerSensor != null)
            {
                sensorManager.Call("registerListener", sensorListener, accelerometerSensor, 3); // 加速度センサー
            }
            else
            {
                Debug.LogWarning("加速度センサーがないため、利用できません。");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("センサー初期化エラー: " + ex.Message);
        }
    }

    // 歩数センサーの変更を受け取る
    public void OnStepSensorChanged(float stepCount)
    {
        if (initialStepCount < 0)
        {
            initialStepCount = (int)stepCount;
        }
        UpdateStepCount((int)stepCount - initialStepCount);  // 歩数更新
    }

    // 加速度センサーの変更を受け取る
    public void OnAccelerometerSensorChanged(Vector3 acceleration)
    {
        float deltaY = Mathf.Abs(acceleration.y - prevYAcceleration);

        // 歩行判定のしきい値を超えると歩数を増加
        if (deltaY > threshold)
        {
            AddStep(1);  // 加速度によって1歩加算
        }

        prevYAcceleration = acceleration.y;
    }

    // センサーのイベントリスナーを解除する
    void OnDestroy()
    {
        if (sensorManager != null && sensorListener != null)
        {
            sensorManager.Call("unregisterListener", sensorListener); // リスナー解除
        }
    }
}
#endif
