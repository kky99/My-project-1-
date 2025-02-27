using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab; // インスペクタでプレハブを割り当て

    void Start()
    {
        // プレハブを (0,0,0) に生成
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
}

