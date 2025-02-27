using UnityEngine;

public class EditorGPS : MonoBehaviour
{
    void Start()
    {
        float latitude = PlayerPrefs.GetFloat("MockLatitude", 43.1896f);
        float longitude = PlayerPrefs.GetFloat("MockLongitude", 141.002f);
        Debug.Log($"仮のGPS座標: {latitude}, {longitude}");
    }
}
