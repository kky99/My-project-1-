using UnityEditor;
using UnityEngine;

// StepCounterBase クラス用のカスタムエディタ
[CustomEditor(typeof(StepCounterBase))]
public class StepCounterEditor : Editor
{
    // エディタが有効になったときの初期化処理
    private void OnEnable()
    {
        Debug.Log("StepCounterEditor initialized");
    }

    // インスペクタのGUIをカスタマイズ
    public override void OnInspectorGUI()
    {
        // デフォルトのインスペクタを描画
        base.OnInspectorGUI();

        // カスタムインスペクタのGUI処理をここに追加
        // 例えば、ボタンやスライダーを追加する場合
        GUILayout.Space(10);
        
        // Resetボタンを表示
        if (GUILayout.Button("Reset Step Counter"))
        {
            StepCounterBase stepCounter = (StepCounterBase)target;
            stepCounter.ResetSteps();  // ResetSteps メソッドを呼び出し
        }
    }
}
