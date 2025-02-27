using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public int expValue = 5;  // 増加する経験値

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerChange player = other.GetComponent<PlayerChange>();
        if (player != null)
        {
            // プレイヤーに経験値を加算
            player.AddExperience(expValue);

            // SaveManager の経験値も更新（SaveManager.Instance が null でないことを確認）
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.experience += expValue;
                // 必要に応じて、即時保存する場合は SaveData() を呼び出す
                SaveManager.Instance.SaveData();
            }

            Destroy(gameObject);
        }
    }
}



