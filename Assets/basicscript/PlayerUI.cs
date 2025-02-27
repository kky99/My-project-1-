using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI expText;   // 経験値表示用のUI
    public TextMeshProUGUI levelText; // レベル表示用のUI
    public TextMeshProUGUI hpText;    // HP表示用のUI

    private PlayerChange playerChange;
    private PlayerHP playerHP; // HPを管理するクラス（Player → PlayerHP）

    void Start()
    {
        // PlayerChange コンポーネントを取得
        playerChange = GameObject.Find("Player")?.GetComponent<PlayerChange>();
        // PlayerHP コンポーネントも取得（HPのため）
        playerHP = GameObject.Find("Player")?.GetComponent<PlayerHP>();

        if (playerChange == null)
        {
            Debug.LogError("PlayerChange コンポーネントが見つかりません！");
        }
        else
        {
            playerChange.LoadData();  // ゲーム開始時にデータを読み込む
        }

        if (playerHP == null)
        {
            Debug.LogError("PlayerHP コンポーネントが見つかりません！");
        }

        UpdateUI();  // 初期UI更新
    }

    // UIを更新
    public void UpdateUI()
    {
        if (playerChange != null)
        {
            int nextLevelExp = playerChange.CalculateNextLevelExp(playerChange.level);

            // 経験値のUIを更新
            if (expText != null)
            {
                expText.text = $"経験値: {playerChange.experience} / 次のレベルまで: {nextLevelExp}";
            }

            // レベルのUIを更新
            if (levelText != null)
            {
                levelText.text = $"レベル: {playerChange.level}";
            }
        }
        else
        {
            Debug.LogError("PlayerChange コンポーネントが見つかりません！ UIを更新できません。");
        }

        // HPのUIを更新
        if (playerHP != null && hpText != null)
        {
            hpText.text = $"HP: {playerHP.currentHP} / {playerHP.maxHP}";
        }
    }
}
