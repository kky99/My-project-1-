using UnityEngine;

public class DestroyOnCollision2D : MonoBehaviour
{
    // プレイヤーのタグを指定
    public string playerTag = "Player";

    // 衝突時に呼ばれるメソッド
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーと衝突した場合
        if (collision.gameObject.CompareTag(playerTag))
        {
            // 自分自身を削除
            Destroy(gameObject);
        }
    }
}


