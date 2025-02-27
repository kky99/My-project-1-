using UnityEngine;
using TMPro;  // TextMeshProUGUI を使用するための名前空間

public class StepDataManager : MonoBehaviour
{
    public static StepDataManager Instance;

    // 保存・表示する歩数・ポイントのデータ
    public int dailySteps;
    public int cumulativeSteps;
    public int points;
    public int stepsPerPoint = 100;
    private int lastPointStep;

    // UI表示用
    public TextMeshProUGUI dailyStepText;
    public TextMeshProUGUI cumulativeStepText;
    public TextMeshProUGUI pointText;

    void Awake()
    {
        // シングルトン化（同じインスタンスを使い回す）
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // シーンを跨いでもこのオブジェクトが破棄されないようにする
        DontDestroyOnLoad(gameObject);

        // StepCounterBase の OnStepCountChanged イベントを購読
        var stepCounterBase = FindFirstObjectByType<StepCounterBase>();  // シーン上の StepCounterBase を検索
        if (stepCounterBase != null)
        {
            stepCounterBase.OnStepCountChanged += OnStepCountChanged;  // イベントに処理を追加
        }

        LoadStepData();  // データをロード
        UpdateUI();  // UIの更新
    }

    // 歩数が変更されたときに呼ばれる
    private void OnStepCountChanged(int steps)
    {
        dailySteps = steps;
        cumulativeSteps += steps;

        // ある歩数に到達したらポイント加算
        if (dailySteps - lastPointStep >= stepsPerPoint)
        {
            points++;
            lastPointStep = dailySteps;
        }

        UpdateUI();
        SaveStepData();  // データを保存
    }

    // UI の表示を更新する
    public void UpdateUI()
    {
        if (dailyStepText != null)
            dailyStepText.text = "今日の歩数: " + dailySteps;
        if (cumulativeStepText != null)
            cumulativeStepText.text = "累計歩数: " + cumulativeSteps;
        if (pointText != null)
            pointText.text = "ポイント: " + points;
    }

    // PlayerPrefs を使ってデータを保存
    public void SaveStepData()
    {
        PlayerPrefs.SetInt("DailySteps", dailySteps);
        PlayerPrefs.SetInt("CumulativeSteps", cumulativeSteps);
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetString("LastStepDate", System.DateTime.Now.ToString("yyyyMMdd"));
        PlayerPrefs.Save();
        Debug.Log("歩数とポイントを保存しました。");
    }

    // PlayerPrefs からデータを読み込み
    public void LoadStepData()
    {
        string lastDate = PlayerPrefs.GetString("LastStepDate", "");
        string today = System.DateTime.Now.ToString("yyyyMMdd");

        if (lastDate == today)
        {
            dailySteps = PlayerPrefs.GetInt("DailySteps", 0);
        }
        else
        {
            dailySteps = 0;
        }

        cumulativeSteps = PlayerPrefs.GetInt("CumulativeSteps", 0);
        points = PlayerPrefs.GetInt("Points", 0);
        lastPointStep = dailySteps;
        Debug.Log("歩数とポイントを読み込みました。");
    }

    void OnDestroy()
    {
        SaveStepData();  // データを保存して終了
    }
}

