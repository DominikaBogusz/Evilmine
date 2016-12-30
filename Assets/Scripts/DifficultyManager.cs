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

public class DifficultyManager : MonoBehaviour
{
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

    private int playerProsperity;
    public int PlayerProsperity
    {
        get { return playerProsperity; }
        set
        {
            playerProsperity = Mathf.Clamp(value, 0, 100);
        }
    }

    private FightResult[] battleResults;

    private int expectedEnemyLevel;
    public int ExpectedEnemyLevel
    {
        get { return expectedEnemyLevel; }
        set
        {
            expectedEnemyLevel = Mathf.Clamp(value, 1, 50);
            EnemySpawner.Instance.DetermineEnemiesToSpawn(expectedEnemyLevel);
        }
    }

    private int interventions;
    private bool previouslyChanged;

    void Start()
    {
        battleResults = new FightResult[3] { new FightResult(FightResult.Result.EMPTY, FightResult.SubResult.EMPTY), new FightResult(FightResult.Result.EMPTY, FightResult.SubResult.EMPTY), new FightResult(FightResult.Result.EMPTY, FightResult.SubResult.EMPTY) };
        ExpectedEnemyLevel = Player.Instance.Attributes.Level.Get();
        PlayerProsperity = 50;
    }

    public void AddFightResult(FightResult.Result result, FightResult.SubResult subResult)
    {
        //Debug.Log(result + ", " + subResult);

        battleResults[2] = battleResults[1];
        battleResults[1] = battleResults[0];
        battleResults[0] = new FightResult(result, subResult);

        CheckIfChangeDifficulty();
    }

    private void CheckIfChangeDifficulty()
    {
        if (!previouslyChanged)
        {
            int intervention = InterventionValue();
            //Debug.Log(intervention);
            if (intervention != 0)
            {
                interventions++;
                ExpectedEnemyLevel += intervention;
                previouslyChanged = true;
            }
        }
        else
        {
            previouslyChanged = false;
        }
    }

    private int InterventionValue()
    {
        if (battleResults[0].result == FightResult.Result.WON && battleResults[1].result == FightResult.Result.WON)
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
}