using UnityEngine;

public class PlayerStatistics : MonoBehaviour {

    private int killCount;
    private int deathCount;

    void Start()
    {
        Player.Instance.DeadEvent += new DeadEventHandler(IncreaseDeathCount); 
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }

    public void IncreaseDeathCount()
    {
        deathCount++;
    }
}
