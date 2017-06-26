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

    [SerializeField] private int minPlayerLevel;
    [SerializeField] private int maxPlayerLevel;

    private int playerLevel;
    public int PlayerLevel
    {
        get { return playerLevel; }
        set
        {
            if(playerLevel > (minPlayerLevel + maxPlayerLevel / 2) && playerLevel < value)
            {
                BonusManager.Instance.TimeBetweenGenerations += value / 100f;
            }
            else if (playerLevel < (minPlayerLevel + maxPlayerLevel / 2) && playerLevel > value)
            {
                BonusManager.Instance.TimeBetweenGenerations -= value / 50f;
            }
            playerLevel = Mathf.Clamp(value, minPlayerLevel, maxPlayerLevel);
        }
    }

    public Dictionary<string, float> EnemyTypesDifficulty;

    void Start()
    {
        PlayerLevel = minPlayerLevel + maxPlayerLevel / 2;
        EnemyTypesDifficulty = new Dictionary<string, float>();
    }

    public enum ModificationFactor { HIGHLY_DECREASE = -2 , SLIGHTLY_DECREASE = -1, SLIGHTLY_INCREASE = 1, HIGHLY_INCREASE = 2 }

    public void ChangeEnemyTypeDifficulty(string enemyType, ModificationFactor factor)
    {
        float value = Random.Range(0.1f * (int)factor, 0.2f * (int)factor);

        if (!EnemyTypesDifficulty.ContainsKey(enemyType))
        {
            EnemyTypesDifficulty.Add(enemyType, 1.0f);
        }
        EnemyTypesDifficulty[enemyType] = Mathf.Clamp(EnemyTypesDifficulty[enemyType] + value, 0.1f, 2.0f);
    }
}
