using UnityEngine;

[System.Serializable]
public class Fight
{
    public bool? won;
    public enum Mark { GOOD, MEDIUM, BAD }
    public Mark? mark;

    public Fight(bool? won, Mark? mark)
    {
        this.won = won;
        this.mark = mark;
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

    private Fight[] battleResults;

    private int expectedEnemyLevel;
    public int ExpectedEnemyLevel
    {
        get { return expectedEnemyLevel; }
        set
        {
            expectedEnemyLevel = Mathf.Clamp(value, 1, 50);
            EnemySpawnManager.Instance.DetermineEnemiesToSpawn(expectedEnemyLevel);
        }
    }

    private int numberOfInterventions;
    private int? previousIntervention;

    void Start()
    {
        battleResults = new Fight[2] { new Fight(null, null), new Fight(null, null) };
        ExpectedEnemyLevel = Player.Instance.Attributes.Level.Get();
        PlayerProsperity = 50;

        LevelManager.Instance.GameplayPersistentObjects.Add(gameObject);
    }

    public void AddFightResult(bool won, Fight.Mark mark)
    {
        Debug.Log(won + ", " + mark);

        battleResults[1] = battleResults[0];
        battleResults[0] = new Fight(won, mark);

        CheckIfChangeDifficulty();
    }

    private void CheckIfChangeDifficulty()
    {
        int intervention = InterventionValue();

        if (intervention < 0 || (intervention > 0 && !(previousIntervention > 0)))
        {
            numberOfInterventions++;
            ExpectedEnemyLevel += intervention;
            Debug.Log(intervention);
            previousIntervention = intervention;
        }
        else if(previousIntervention > 0)
        {
            previousIntervention = null;
        }
    }

    private int InterventionValue()
    {
        if (battleResults[0].won == true && battleResults[1].won == true)
        {
            if ((battleResults[1].mark == Fight.Mark.BAD || battleResults[1].mark == Fight.Mark.MEDIUM) && battleResults[0].mark == Fight.Mark.BAD)
            {
                return -1;
            }
            else if ((battleResults[1].mark == Fight.Mark.BAD || battleResults[1].mark == Fight.Mark.MEDIUM) && battleResults[0].mark == Fight.Mark.MEDIUM)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else if (battleResults[0].won == false)
        {
            return -1;
        }
        return 0;
    }
}