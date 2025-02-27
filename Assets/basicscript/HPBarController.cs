using UnityEngine;
using UnityEngine.UI;

public class HPBarController : MonoBehaviour
{
    public Image hpBar; // HPバーのImage
    public float maxHP = 100f;
    private float currentHP;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HPが0未満や最大値を超えないようにする
        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        hpBar.fillAmount = currentHP / maxHP;
    }
}

