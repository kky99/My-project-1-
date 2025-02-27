#if UNITY_IOS
using UnityEngine;

public class StepCounterIOS : StepCounterBase
{
    public override void Initialize()
    {
        Debug.Log("iOSの歩数取得は外部プラグインが必要です。");
        // CoreMotion を使う場合、iOSプラグインを作成し、ネイティブコードで処理する
    }

    public void OnIOSStepCountReceived(int stepCount)
    {
        UpdateStepCount(stepCount);
    }
}
#endif

