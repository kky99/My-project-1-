using UnityEngine;

public class StepManager : MonoBehaviour
{
    private StepCounterBase stepCounter;

    void Start()
    {
#if UNITY_ANDROID
        stepCounter = gameObject.AddComponent<StepCounterAndroid>();
#elif UNITY_IOS
        stepCounter = gameObject.AddComponent<StepCounterIOS>();
#else
        // エディタースクリプトを除外
        stepCounter = gameObject.AddComponent<StepCounterBase>();  // または、適切なデフォルト実装を使用
#endif

        stepCounter.Initialize();
        stepCounter.OnStepCountChanged += OnStepCountUpdated;
    }

    void OnStepCountUpdated(int newSteps)
    {
        Debug.Log("現在の歩数: " + newSteps);
    }
}
