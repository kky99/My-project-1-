using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingPanel; // ロード画面のUIパネル
    public Slider progressBar; // 進捗バー（オプション）

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string MainMenuScene)
    {
        loadingPanel.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(MainMenuScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            
            if (operation.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1f); // 少し待つ
                operation.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}
