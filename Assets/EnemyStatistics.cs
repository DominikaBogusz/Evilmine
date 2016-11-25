using UnityEngine;
using System.Collections.Generic;

public class EnemyStatistics : MonoBehaviour {

    private Enemy enemy;
    private Player player;

    public int KillCount { get; set; }
    public int DamageMade { get; set; }
    private int damageMadeInPreviousBattles;
    public int DamageReceived { get; set; }
    private int damageReceivedInPreviousBattles;

    public List<float> BattleTimes;
    private float BattleTimer;
    public bool StartBattleTimer { get; set; }
    public bool StopBattleTimer { get; set; }

    public List<BattleResult> BattleResults;
    public enum BattleResult { WON_EASILY, WON_MIDDLING, WON_HARDLY, LOST_NEARLY, LOST_MIDDLING, LOST_ENTIRELY, RUN_AWAY, IGNORED }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = Player.Instance;
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

            Debug.Log(EvaluateBattle());
            BattleResults.Add(EvaluateBattle());
        }
    }

    public BattleResult EvaluateBattle()
    {
        int damageMade = DamageMade - damageMadeInPreviousBattles;
        int damageReceived = DamageReceived - damageReceivedInPreviousBattles;

        damageMadeInPreviousBattles += damageMade;
        damageReceivedInPreviousBattles += damageReceived;

        float playerMaxLife = player.Attributes.initialHealth;
        float enemyMaxLife = enemy.Attributes.initialHealth;

        if (enemy.IsDead)
        {
            if (damageMade > playerMaxLife * 0.7)
            {
                return BattleResult.WON_HARDLY;
            }
            else if (damageMade <= playerMaxLife * 0.7 && DamageMade >= playerMaxLife * 0.4)
            {
                return BattleResult.WON_MIDDLING;
            }
            else if (damageMade < playerMaxLife * 0.4)
            {
                return BattleResult.WON_EASILY;
            }
        }
        else if (player.IsDead)
        {
            if (damageReceived > enemyMaxLife * 0.7)
            {
                return BattleResult.LOST_NEARLY;
            }
            else if (damageReceived <= enemyMaxLife * 0.7 && DamageReceived >= enemyMaxLife * 0.4)
            {
                return BattleResult.LOST_MIDDLING;
            }
            else if (damageReceived < enemyMaxLife * 0.4)
            {
                return BattleResult.LOST_ENTIRELY;
            }
        }
        else if (damageReceived > 0)
        {
            return BattleResult.RUN_AWAY;
        }

        return BattleResult.IGNORED;
    }
}
