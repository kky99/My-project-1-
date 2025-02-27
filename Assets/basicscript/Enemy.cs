using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageAmount = 2; // ダメージ量

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // プレイヤーの HP を減らす
            PlayerHP playerHP = collision.GetComponent<PlayerHP>();
            if (playerHP != null)
            {
                playerHP.TakeDamage(damageAmount);
            }

            // 敵を消す
            Destroy(gameObject);
        }
    }
}

