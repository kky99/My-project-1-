using UnityEngine;
using TMPro; // TextMeshPro を使うために必要

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI messageText; // テキストを操作するための変数

    void Start()
    {
        messageText.text = "hello!"; // 初期テキスト
    }

    void Update()
    {
        // エンターキーを押したらテキストを変更
        if (Input.GetKeyDown(KeyCode.Return))
        {
            messageText.text = "次のテキストへ！";
        }
    }
}

