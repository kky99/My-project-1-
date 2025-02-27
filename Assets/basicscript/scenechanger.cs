using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenechanger : MonoBehaviour
{
    // クリックされた時にシーンを切り替える
    void OnMouseDown()
    {
        // シーンを切り替える
        SceneManager.LoadScene("MainMenuScene"); // "MainGame" は切り替えたいシーン名
    }
}
