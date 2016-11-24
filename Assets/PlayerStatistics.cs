using UnityEngine;

public class PlayerStatistics : MonoBehaviour {

    public int KillCount { get; set; }
    public int DeathCount { get; set; }

    void Start()
    {
        Player.Instance.DeadEvent += new DeadEventHandler(IncreaseDeathCount);
    }

    private void IncreaseDeathCount()
    {
        DeathCount++;
    }
}
