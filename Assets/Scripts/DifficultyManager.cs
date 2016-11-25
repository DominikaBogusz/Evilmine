using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    private static DifficultyManager instance;
    public static DifficultyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DifficultyManager>();
            }
            return instance;
        }
    }

    public Dictionary<string, float> EnemiesDifficultyLevel;

    void Start()
    {
        EnemiesDifficultyLevel = new Dictionary<string, float>();
    }

    public void HighlyIncreaseDifficultyLevel(string enemyName)
    {
        float incValue = Random.Range(0.2f, 0.4f);
        ChangeDifficultyLevel(enemyName, incValue);
    }

    public void SlightlyIncreaseDifficultyLevel(string enemyName)
    {
        float incValue = Random.Range(0.1f, 0.2f);
        ChangeDifficultyLevel(enemyName, incValue);
    }

    public void HighlyDecreaseDifficultyLevel(string enemyName)
    {
        float decValue = Random.Range(-0.4f, -0.2f);
        ChangeDifficultyLevel(enemyName, decValue);
    }

    public void SlightlyDecreaseDifficultyLevel(string enemyName)
    {
        float decValue = Random.Range(-0.2f, -0.1f);
        ChangeDifficultyLevel(enemyName, decValue);
    }

    private void ChangeDifficultyLevel(string enemyName, float value)
    {
        if (!EnemiesDifficultyLevel.ContainsKey(enemyName))
        {
            EnemiesDifficultyLevel.Add(enemyName, 1.0f);
        }
        EnemiesDifficultyLevel[enemyName] = Mathf.Clamp(EnemiesDifficultyLevel[enemyName] + value, 0.1f, 2.0f);
    }
}
