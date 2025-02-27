using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // SaveManager がシーンに存在しない場合、新しく作成
                GameObject obj = new GameObject("SaveManager");
                _instance = obj.AddComponent<SaveManager>();
                DontDestroyOnLoad(obj);
                Debug.Log("[SaveManager] インスタンスが存在しなかったので新しく作成しました。");
            }
            return _instance;
        }
    }

    public int points;
    public int experience;
    public int level;
    public int currentHP;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetInt("Experience", experience);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("CurrentHP", currentHP);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        points = PlayerPrefs.GetInt("Points", 0);
        experience = PlayerPrefs.GetInt("Experience", 0);
        level = PlayerPrefs.GetInt("Level", 1);
        currentHP = PlayerPrefs.GetInt("CurrentHP", 100);

        Debug.Log($"[SaveManager] データを読み込み: Points={points}, Experience={experience}, Level={level}, HP={currentHP}");
    }

    public void ResetData()
    {
        points = 0;
        experience = 0;
        level = 1;
        currentHP = 100;
        SaveData();
        Debug.Log("[SaveManager] データをリセットしました。");
    }
}
