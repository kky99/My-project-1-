using UnityEngine;

public class Food : MonoBehaviour
{
    public int hp = 3; // 食べる前の耐久値
    public int healAmount = 2; // 回復量

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"Food の HP: {hp}");

        if (hp <= 0)
        {
            Destroy(gameObject); // HP が 0 以下なら消える
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // プレイヤーが触れたら
        {
            PlayerHP player = other.GetComponent<PlayerHP>();
            if (player != null)
            {
                player.Heal(healAmount); // プレイヤーの HP を回復
                Destroy(gameObject); // 食べたら消える
            }
        }
    }
}
