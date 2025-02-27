using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public int experience = 0;  // 現在の経験値
    public int level = 1;       // 現在のレベル
    private PlayerUI playerUI;  // PlayerUI 参照
    private SaveManager saveManager;  // SaveManager 参照

    void Awake()
    {
        // SaveManager インスタンスを取得
        saveManager = SaveManager.Instance;

        if (saveManager == null)
        {
            Debug.LogError("SaveManagerがnullです！ シーンにSaveManagerが存在するか確認してください。");
            return;  // SaveManagerがない場合、処理を中断
        }

        // データをロード（Awakeの時点で行う）
        LoadData();
    }

    void Start()
    {
        // PlayerUI コンポーネントを取得
        playerUI = GameObject.Find("PlayerUI")?.GetComponent<PlayerUI>();

        if (playerUI == null)
        {
            Debug.LogError("PlayerUI コンポーネントが見つかりません！");
        }

        // UIを初期更新
        UpdateUI();
    }

    // 経験値を加算し、レベルアップを確認
    public void AddExperience(int amount)
    {
        if (saveManager == null)
        {
            Debug.LogError("SaveManagerがnullです！ 経験値の追加ができません。");
            return;
        }

        experience += amount;
        CheckLevelUp();  // レベルアップの確認

        // UIを更新
        UpdateUI();

        // SaveManagerにデータを保存
        saveManager.experience = experience;
        saveManager.level = level;
        saveManager.SaveData();
    }

    // レベルアップを確認
    void CheckLevelUp()
    {
        int nextLevelExp = CalculateNextLevelExp(level);
        while (experience >= nextLevelExp)
        {
            experience -= nextLevelExp;
            level++;
            Debug.Log("レベルアップ！ 現在のレベル: " + level);
            nextLevelExp = CalculateNextLevelExp(level);
        }
    }

    // 次のレベルに必要な経験値を計算
    public int CalculateNextLevelExp(int currentLevel)
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(1.2f, currentLevel - 1));
    }

    // データを読み込む
    public void LoadData()
    {
        if (saveManager != null)
        {
            experience = saveManager.experience;
            level = saveManager.level;
            Debug.Log($"読み込んだデータ: Experience={experience}, Level={level}");
        }
        else
        {
            Debug.LogError("SaveManagerがnullです！ データを読み込めません。");
        }
    }

    // UIを更新
    void UpdateUI()
    {
        if (playerUI != null)
        {
            playerUI.UpdateUI();
        }
        else
        {
            Debug.LogError("PlayerUI コンポーネントが見つかりません！");
        }
    }
}
