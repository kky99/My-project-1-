using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;  // キャラクターのスプライト
    public Sprite[] levelSprites;  // レベルごとのスプライトを入れる配列

    private PlayerChange playerChange;

    void Start()
    {
        playerChange = GetComponent<PlayerChange>();
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        if (levelSprites != null && playerChange.level - 1 < levelSprites.Length)
        {
            spriteRenderer.sprite = levelSprites[playerChange.level - 1];
            Debug.Log("見た目変更: レベル " + playerChange.level);
        }
    }
}
