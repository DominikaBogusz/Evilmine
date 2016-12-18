using UnityEngine;
using System.Collections.Generic;

public class EnemyStatistics : MonoBehaviour {

    private Enemy enemy;
    private Player player;

    public int KillCount { get; set; }
    public float DamageMade { get; set; }
    private float damageMadeInPreviousBattles;
    public float DamageReceived { get; set; }
    private float damageReceivedInPreviousBattles;

    private List<float> BattleTimes;
    private float BattleTimer;
    public bool StartBattleTimer { get; set; }
    public bool StopBattleTimer { get; set; }

    public List<BattleResult> BattleResults;
    public enum BattleResult { WON_EASILY, WON_MIDDLING, WON_HARDLY, LOST_NEARLY, LOST_MIDDLING, LOST_ENTIRELY, RUN_AWAY_HEALTHY, RUN_AWAY_BLEEDING, IGNORED }

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

            BattleResults.Add(EvaluateBattle());
            EvaluateEnemyDifficultyLevel();
        }
    }

    private BattleResult EvaluateBattle()
    {
        float damageMade = DamageMade - damageMadeInPreviousBattles;
        float damageReceived = DamageReceived - damageReceivedInPreviousBattles;

        damageMadeInPreviousBattles += damageMade;
        damageReceivedInPreviousBattles += damageReceived;

        float playerMaxLife = player.Attributes.Health.max;
        float enemyMaxLife = enemy.Attributes.Health.max;

        if (enemy.IsDead)
        {
            if (damageMade > playerMaxLife * 0.7)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.WON, FightResult.SubResult.BAD);
                return BattleResult.WON_HARDLY;
            }
            else if (damageMade <= playerMaxLife * 0.7 && damageMade >= playerMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.WON, FightResult.SubResult.MEDIUM);
                DifficultyManager.Instance.PlayerLevel += 2;
                return BattleResult.WON_MIDDLING;
            }
            else if (damageMade < playerMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.WON, FightResult.SubResult.GOOD);
                DifficultyManager.Instance.PlayerLevel += 4;
                return BattleResult.WON_EASILY;
            }
        }
        else if (player.IsDead)
        {
            if (damageReceived > enemyMaxLife * 0.7)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.GOOD);
                return BattleResult.LOST_NEARLY;
            }
            else if (damageReceived <= enemyMaxLife * 0.7 && damageReceived >= enemyMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.MEDIUM);
                DifficultyManager.Instance.PlayerLevel -= 2;
                return BattleResult.LOST_MIDDLING;
            }
            else if (damageReceived < enemyMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.BAD);
                DifficultyManager.Instance.PlayerLevel -= 4;
                return BattleResult.LOST_ENTIRELY;
            }
        }
        else if (damageReceived > 0)
        {
            if(player.Attributes.Health.Get() < playerMaxLife * 0.4)
            {
                DifficultyManager.Instance.PlayerLevel -= 4;
                return BattleResult.RUN_AWAY_BLEEDING;
            }
            else
            {
                return BattleResult.RUN_AWAY_HEALTHY;
            }
        }

        return BattleResult.IGNORED;
    }

    private void EvaluateEnemyDifficultyLevel()
    {
        switch (BattleResults[BattleResults.Count - 1])
        {
            case BattleResult.WON_EASILY:
                DifficultyManager.Instance.ChangeEnemyTypeDifficulty(enemy.name, DifficultyManager.ModificationFactor.HIGHLY_INCREASE);
                    return;
            case BattleResult.WON_MIDDLING:
                DifficultyManager.Instance.ChangeEnemyTypeDifficulty(enemy.name, DifficultyManager.ModificationFactor.SLIGHTLY_INCREASE);
                    return;
        }

        int lostEntirelyCount = 0, lostMiddlingCount = 0, lostNearlyCount = 0;
        foreach (BattleResult battleResult in BattleResults)
        {
            if(battleResult == BattleResult.LOST_ENTIRELY)
            {
                lostEntirelyCount++;
            }
            else if(battleResult == BattleResult.LOST_MIDDLING)
            {
                lostMiddlingCount++;
            }
            else if(battleResult == BattleResult.LOST_NEARLY)
            {
                lostNearlyCount++;
            }
        }

        if(lostEntirelyCount + lostMiddlingCount >= 2)
        {
            DifficultyManager.Instance.ChangeEnemyTypeDifficulty(enemy.name, DifficultyManager.ModificationFactor.HIGHLY_DECREASE);
        }
        else if(lostMiddlingCount + lostNearlyCount >= 2)
        {
            DifficultyManager.Instance.ChangeEnemyTypeDifficulty(enemy.name, DifficultyManager.ModificationFactor.SLIGHTLY_DECREASE);
        }

        if(lostEntirelyCount + lostMiddlingCount >= 4)
        {
            enemy.Attributes.AccomodateToDifficultyLevel(enemy.name);
        }
        else if(lostMiddlingCount + lostNearlyCount >= 4)
        {
            enemy.Attributes.AccomodateToDifficultyLevel(enemy.name);
        }
    }
}
