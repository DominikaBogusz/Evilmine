using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour {

    public static Dictionary<string, float> EnemyTypesDifficulty;

    void Start()
    {
        EnemyTypesDifficulty = new Dictionary<string, float>();
    }

    public enum ModificationFactor { HIGHLY_DECREASE = -2 , SLIGHTLY_DECREASE = -1, SLIGHTLY_INCREASE = 1, HIGHLY_INCREASE = 2 }

    public static void ChangeEnemyTypeDifficulty(string enemyType, ModificationFactor factor)
    {
        float value = Random.Range(0.1f * (int)factor, 0.2f * (int)factor);

        if (!EnemyTypesDifficulty.ContainsKey(enemyType))
        {
            EnemyTypesDifficulty.Add(enemyType, 1.0f);
        }
        EnemyTypesDifficulty[enemyType] = Mathf.Clamp(EnemyTypesDifficulty[enemyType] + value, 0.1f, 2.0f);
    }

    public static void ChangeEnemyDifficulty(Enemy enemy)
    {
        enemy.Attributes.AccomodateToDifficultyLevel(enemy.name);
    }
}
