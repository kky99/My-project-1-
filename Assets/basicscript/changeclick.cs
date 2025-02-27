using UnityEngine;
using UnityEngine.SceneManagement;

public class changeclick : MonoBehaviour
{
    public string sceneName = "NextScene";  // 切り替えるシーン名

    void OnMouseDown()
    {
        Debug.Log(gameObject.name + " がクリックされました！");
        SceneManager.LoadScene(sceneName);
    }
}
