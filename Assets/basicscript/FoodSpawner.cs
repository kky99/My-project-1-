using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab; // 餌のプレハブ
    public int maxFoodCount = 10; // 配置する最大の餌の数
    public Vector2 spawnArea = new Vector2(5f, 5f); // 配置範囲
    private List<GameObject> foodList = new List<GameObject>(); // 現在の餌リスト

    void Start()
    {
        SpawnFood(maxFoodCount);
        StartCoroutine(CheckFoodRoutine()); // 餌の数を定期的にチェック
    }

    void SpawnFood(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(-spawnArea.x, spawnArea.x),
                Random.Range(-spawnArea.y, spawnArea.y),
                0f // 2DゲームならZ軸は0
            );

            GameObject newFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
            foodList.Add(newFood); // リストに追加
        }
    }

    IEnumerator CheckFoodRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f); // 2秒ごとにチェック

            // リストから削除されたオブジェクトをクリーンアップ
            foodList.RemoveAll(item => item == null);

            // 足りなくなった分の餌を補充
            int missingFood = maxFoodCount - foodList.Count;
            if (missingFood > 0)
            {
                SpawnFood(missingFood);
            }
        }
    }
}
