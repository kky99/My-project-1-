using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // ゲーム開始時に入力を受け付ける
    void Update()
    {
        // クリックまたはタップでスタート
        if (Input.GetMouseButtonDown(0)) // 左クリック（またはタップ）
        {
            StartGameScene();
        }

        // エンターキーでスタート
        if (Input.GetKeyDown(KeyCode.Return)) // エンターキー
        {
            StartGameScene();
        }
    }

    // シーン切り替えメソッド
    void StartGameScene()
    {
        SceneManager.LoadScene("LoadScene"); // "ゲームシーン名
    }
}
