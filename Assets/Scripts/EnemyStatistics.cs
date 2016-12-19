using UnityEngine;

public class EnemyStatistics : MonoBehaviour {

    private Enemy enemy;
    private Player player;

    private int killCount;

    public float DamageMade { get; set; }
    private float damageMadeInPreviousBattles;
    public float DamageReceived { get; set; }
    private float damageReceivedInPreviousBattles;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = Player.Instance;
    }

    public void IncreaseKillCount()
    {
        killCount++;
    }

    public void EvaluateFight()
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
                DifficultyManager.Instance.PlayerProsperity -= 5;
            }
            else if (damageMade <= playerMaxLife * 0.7 && damageMade >= playerMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.WON, FightResult.SubResult.MEDIUM); 
            }
            else if (damageMade < playerMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.WON, FightResult.SubResult.GOOD);
                DifficultyManager.Instance.PlayerProsperity += 5;
            }
        }
        else if (player.IsDead)
        {
            if (damageReceived > enemyMaxLife * 0.7)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.GOOD);
            }
            else if (damageReceived <= enemyMaxLife * 0.7 && damageReceived >= enemyMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.MEDIUM);
            }
            else if (damageReceived < enemyMaxLife * 0.4)
            {
                DifficultyManager.Instance.AddFightResult(FightResult.Result.LOST, FightResult.SubResult.BAD);
            }
        }
    }
}
