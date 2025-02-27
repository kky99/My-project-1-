using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHP = 10; // 最大 HP
    public int currentHP; // 現在の HP
    public Image hpBar; // HPバーの Image
    private PlayerUI playerUI; // UI更新用

    void Start()
    {
        currentHP = maxHP; // HP を最大値で初期化
        playerUI = Object.FindFirstObjectByType<PlayerUI>(); // PlayerUI を探して取得
        UpdateHPBar(); // 初期のHPバー更新
        playerUI?.UpdateUI(); // 初期のUI更新
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0) currentHP = 0;

        Debug.Log($"プレイヤーが {damage} ダメージを受けた！ 現在の HP: {currentHP}");

        UpdateHPBar();
        playerUI?.UpdateUI(); // HP変化をUIに即時反映

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // HP を回復する
    public void Heal(int healAmount)
    {
        currentHP += healAmount;
        if (currentHP > maxHP) currentHP = maxHP;

        Debug.Log($"プレイヤーが {healAmount} 回復した！ 現在の HP: {currentHP}");

        UpdateHPBar();
        playerUI?.UpdateUI(); // HP変化をUIに即時反映
    }

    // HPバーの更新
    void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHP / maxHP;
        }
    }

    void Die()
    {
        Debug.Log("プレイヤーが倒れた！");
    }
}
