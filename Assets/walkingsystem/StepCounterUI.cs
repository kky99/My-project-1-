using UnityEngine;
using TMPro;

public class StepCounterUI : MonoBehaviour
{
    public TextMeshProUGUI stepText;           // 歩数表示用UI
    public TextMeshProUGUI pointText;          // ポイント表示用UI
    public TextMeshProUGUI cumulativeStepText; // 累計歩数表示用UI

    private StepCounterBase stepCounter;      // StepCounterのインスタンス

    void Start()
    {
    // StepCounterのインスタンスを取得
    stepCounter = Object.FindFirstObjectByType<StepCounterBase>();

    // StepDataManagerで保存された歩数データを読み込む
    LoadStepData();

    // StepCounterの歩数が変更されるたびにUpdateUIを呼び出すイベントを登録
    if (stepCounter != null)
    {
        stepCounter.OnStepCountChanged += UpdateUI;
    }
    else
    {
        Debug.LogError("StepCounterインスタンスが見つかりませんでした。");
    }
    }

    void LoadStepData()
    {
    // StepDataManager からデータを読み込む
    int savedSteps = PlayerPrefs.GetInt("SavedSteps", 0);  // "SavedSteps" というキーで保存された歩数を取得
    stepCounter.UpdateStepCount(savedSteps);  // StepCounterの歩数を更新
    UpdateUI(savedSteps);  // UIの更新
    }


    // UpdateUIメソッド
    void UpdateUI(int steps)
    {
        int points = steps / 100;  // 100歩ごとに1ポイント（例）
        int cumulativeSteps = steps;  // 累計歩数をそのまま表示（例）

        // UIの更新
        UpdateStepUI(stepText," "+ steps);
        UpdateStepUI(pointText, "ポイント: " + points);
        UpdateStepUI(cumulativeStepText, "累計歩数: " + cumulativeSteps);
    }

    // UI更新を簡潔にするためのヘルパーメソッド
    private void UpdateStepUI(TextMeshProUGUI textElement, string text)
    {
        if (textElement != null)
        {
            textElement.text = text;
        }
    }

    // イベントの解除（オブジェクトが破棄される前）
    void OnDestroy()
    {
        if (stepCounter != null)
        {
            stepCounter.OnStepCountChanged -= UpdateUI;
        }
    }
}
