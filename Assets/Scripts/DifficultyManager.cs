using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FightResult
{
    public enum Result { EMPTY, WON, LOST }
    public enum SubResult { EMPTY, GOOD, MEDIUM, BAD }

    public Result result;
    public SubResult subResult;

    public FightResult(Result result, SubResult subResult)
    {
        this.result = result;
        this.subResult = subResult;
    }
}

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

    public int ExpectedEnemyLevel { get; set; }
    public int PlayerProsperity { get; set; }

    private FightResult[] battleResults;

    private int interventions;

    void Start()
    {
        battleResults = new FightResult[3];

        ExpectedEnemyLevel = Player.Instance.Attributes.Level.Get();

        PlayerLevel = minPlayerLevel + maxPlayerLevel / 2;
        EnemyTypesDifficulty = new Dictionary<string, float>();
    }

    public void AddFightResult(FightResult.Result result, FightResult.SubResult subResult)
    {
        battleResults[2] = battleResults[1];
        battleResults[1] = battleResults[0];
        battleResults[0] = new FightResult(result, subResult);

        CheckIfChangeDifficulty();
    }

    private void CheckIfChangeDifficulty()
    {
        int intervention = InterventionValue();
        if (intervention != 0)
        {
            interventions++;
            if(interventions % 2 == 0)
            {
                ExpectedEnemyLevel += intervention;
            }            
        }
    }

    private int InterventionValue()
    {
        if(battleResults[0].result == FightResult.Result.WON && battleResults[1].result == FightResult.Result.WON)
        {
            if (battleResults[0].subResult == FightResult.SubResult.GOOD && battleResults[1].subResult == FightResult.SubResult.GOOD)
            {
                return 1;
            }
            if (battleResults[2].result == FightResult.Result.WON)
            {
                return 1;
            }
        }
        else if (battleResults[0].result == FightResult.Result.LOST && battleResults[1].result == FightResult.Result.LOST)
        {
            if (battleResults[0].subResult == FightResult.SubResult.BAD && battleResults[1].subResult == FightResult.SubResult.BAD)
            {
                return -1;
            }
            if (battleResults[2].result == FightResult.Result.LOST)
            {
                return -1;
            }
        }
        return 0;
    }

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
