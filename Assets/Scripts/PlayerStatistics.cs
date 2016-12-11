using UnityEngine;

public class PlayerStatistics : MonoBehaviour {

    private int killCount;
    private int deathCount;

    [SerializeField] private int minLevel;
    [SerializeField] private int maxLevel;

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            if(level > (minLevel + maxLevel / 2) && level < value)
            {
                BonusManager.Instance.TimeBetweenGenerations += value / 100f;
            }
            else if (level < (minLevel + maxLevel / 2) && level > value)
            {
                BonusManager.Instance.TimeBetweenGenerations -= value / 50f;
            }
            level = Mathf.Clamp(value, minLevel, maxLevel);
        }
    }

    void Start()
    {
        Player.Instance.DeadEvent += new DeadEventHandler(IncreaseDeathCount);

        Level = minLevel + maxLevel / 2;
    }

    public void IncreaseKillCount()
    {
        Level++;
        killCount++;
    }

    public void IncreaseDeathCount()
    {
        Level--;
        deathCount++;
    }
}
