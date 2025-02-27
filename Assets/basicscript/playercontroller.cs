using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // 移動速度
    private Vector2 touchStartPos;  // タッチ開始位置
    private Vector2 touchEndPos;    // タッチ終了位置
    private Vector2 swipeDelta;     // スワイプの移動量
    public float swipeSensitivity = 0.01f; // スワイプ感度

    void Update()
    {
        // タッチ入力がある場合
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 最初のタッチを取得

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // タッチ開始位置を記録
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    // タッチが動いた場合、スワイプの移動量を計算
                    touchEndPos = touch.position;
                    swipeDelta = touchEndPos - touchStartPos;

                    // スワイプをスクリーン座標からワールド座標に変換
                    // スワイプ量の調整とスクリーン座標系からワールド座標系に変換
                    swipeDelta.x = swipeDelta.x / Screen.width * Camera.main.orthographicSize * 2;
                    swipeDelta.y = swipeDelta.y / Screen.height * Camera.main.orthographicSize * 2;

                    // 移動量に基づいてプレイヤーを動かす
                    Vector3 swipeMove = new Vector3(swipeDelta.x, swipeDelta.y, 0) * swipeSensitivity;

                    // プレイヤーを移動
                    transform.position += swipeMove;

                    // タッチ開始位置を更新
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    // タッチ終了時にスワイプをリセット
                    swipeDelta = Vector2.zero;
                    break;
            }
        }

        // 方向キーによる移動
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 方向キーによる移動量
        Vector3 keyboardMove = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;

        // プレイヤーを移動
        transform.position += keyboardMove;
    }
}
