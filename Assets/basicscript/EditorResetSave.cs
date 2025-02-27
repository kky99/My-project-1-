#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EditorResetSave
{
    static EditorResetSave()
    {
        EditorApplication.playModeStateChanged += ResetDataOnExit;
    }

    private static void ResetDataOnExit(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.ResetData();
                Debug.Log("エディタのプレイモード終了時にセーブデータをリセットしました。");
            }
        }
    }
}
#endif

