using UnityEngine;
using TMPro;  // TextMeshProを使用するために必要

public class PointCounter : MonoBehaviour
{
    public TextMeshProUGUI pointsText;  // インスペクターでUIのTextMeshProUGUIを設定
    private int points = 0;  // 初期ポイント

    // ポイントを表示するメソッド
    public void ShowPoints()
    {
        pointsText.text = "ポイント: " + points.ToString();  // UIに表示
    }

    // ポイントを加算するメソッド
    public void AddPoint(int pointsToAdd)
    {
        points += pointsToAdd;  // ポイントを加算
        ShowPoints();  // ポイントを表示更新
    }

    // 現在のポイントを返すメソッド
    public int GetPoints()
    {
        return points;
    }

    // ステップ数に基づいてポイントを計算して更新するメソッド
    public void UpdatePoints(int steps)
    {
        // 100歩ごとに1ポイント（例）
        int newPoints = steps / 100;  
        points = newPoints;  // 計算されたポイントで更新
        ShowPoints();  // ポイントを表示更新
    }
}
