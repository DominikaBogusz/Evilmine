using UnityEngine;
using System.Collections.Generic;

public class EnemyStatistics : MonoBehaviour {

    public int KillCount { get; set; }
    public int DamageMade { get; set; }
    public int DamageReceived { get; set; }

    public List<float> BattleTimes;
    private float BattleTimer;
    public bool StartBattleTimer { get; set; }
    public bool StopBattleTimer { get; set; }

    void Start()
    {
        BattleTimes = new List<float>();
    }

    void Update()
    {
        CheckBattleTimes();
    }

    private void CheckBattleTimes()
    {
        if (StartBattleTimer)
        {
            BattleTimer += Time.deltaTime;
        }

        if (StopBattleTimer)
        {
            StartBattleTimer = false;
            StopBattleTimer = false;

            BattleTimes.Add(BattleTimer);
            BattleTimer = 0f;
        }
    }
}
