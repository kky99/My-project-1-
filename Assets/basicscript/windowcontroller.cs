using UnityEngine;

public class WindowController : MonoBehaviour
{ public GameObject window; // 開閉するウィンドウ（パネル）

    public void ToggleWindow()
    {
        window.SetActive(!window.activeSelf); // 現在の状態を反転
    }

}

