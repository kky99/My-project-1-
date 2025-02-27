using UnityEngine;

public class StepCounterBase : MonoBehaviour
{
    // 歩数が変更されたときに呼び出されるデリゲート
    public delegate void StepCountChanged(int steps);
    public event StepCountChanged OnStepCountChanged;  // 歩数変更時のイベント

    protected int steps = 0;  // 歩数（protectedにして子クラスからアクセスできるようにする）

    // 初期化処理
    public virtual void Initialize()
    {
        steps = 0;  // 歩数の初期化
        Debug.Log("StepCounter が初期化されました。歩数: " + steps);
    }

    // 歩数を加算するメソッド
    public void AddStep(int amount)
    {
    steps += amount;
    OnStepCountChanged?.Invoke(steps);  // 歩数が更新されたときにイベントを発火

    // 歩数をPlayerPrefsに保存
    PlayerPrefs.SetInt("SavedSteps", steps);
    PlayerPrefs.Save();
    }


    // 歩数を更新するメソッド
    public void UpdateStepCount(int newStepCount)
    {
        steps = newStepCount;
        OnStepCountChanged?.Invoke(steps);  // 歩数が更新されたときにイベントを発火
    }

    // 歩数をリセットするメソッド
    public void ResetSteps()
    {
        steps = 0;
        OnStepCountChanged?.Invoke(steps);  // 歩数がリセットされたときにイベントを発火
        Debug.Log("歩数がリセットされました。");
    }

    // 現在の歩数を取得するメソッド
    public int GetSteps()
    {
        return steps;
    }

    // Updateメソッドを使ってスペースキー押下を監視
    void Update()
    {
        // スペースキーが押されたとき
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddStep(1);  // 歩数を1増加
        }
    }
}
