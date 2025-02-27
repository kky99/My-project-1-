using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // 配置したいオブジェクト（ポイントなど）
    public int minObjects = 1;        // 最小配置数
    public int maxObjects = 3;       // 最大配置数
    public float spawnRangeX = 10f;   // X軸の配置範囲
    public float spawnRangeY = 5f;    // Y軸の配置範囲
    public float spawnInterval = 5f;  // 次にオブジェクトを生成するまでの間隔（秒）
    private float lastSpawnTime = 0f; // 最後に生成した時刻

    void Update()
    {
        // 一定時間ごとにオブジェクトを生成
        if (Time.time - lastSpawnTime > spawnInterval)
        {
            SpawnObjects();
            lastSpawnTime = Time.time;  // 最後にオブジェクトを生成した時刻を更新
        }
    }

    void SpawnObjects()
    {
        // 配置するオブジェクトの数をランダムに決定
        int objectCount = Random.Range(minObjects, maxObjects + 1);

        // ランダムな位置にオブジェクトを配置
        for (int i = 0; i < objectCount; i++)
        {
            // ランダムな位置を生成
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            float randomY = Random.Range(-spawnRangeY, spawnRangeY);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0); // Zは0で平面に配置

            // オブジェクトを配置
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
