using UnityEngine;

public class EnemyStatistics : MonoBehaviour {

    private Enemy enemy;
    private Player player;

    private int killCount;

    public float DamageMade { get; set; }
    public float DamageReceived { get; set; }

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
        float playerMaxLife = player.Attributes.Health.max;
        float enemyMaxLife = enemy.Attributes.ActualMaxHealth;

        if (enemy.IsDead)
        {
            if (DamageMade > playerMaxLife * 0.6)
            {
                DifficultyManager.Instance.AddFightResult(true, Fight.Mark.BAD);
                DifficultyManager.Instance.PlayerProsperity -= 5;
            }
            else if (DamageMade <= playerMaxLife * 0.6 && DamageMade >= playerMaxLife * 0.3)
            {
                DifficultyManager.Instance.AddFightResult(true, Fight.Mark.MEDIUM); 
            }
            else if (DamageMade < playerMaxLife * 0.3)
            {
                DifficultyManager.Instance.AddFightResult(true, Fight.Mark.GOOD);
                DifficultyManager.Instance.PlayerProsperity += 5;
            }
        }
        else if (player.IsDead)
        {
            if (DamageReceived > enemyMaxLife * 0.6)
            {
                DifficultyManager.Instance.AddFightResult(false, Fight.Mark.GOOD);
            }
            else if (DamageReceived <= enemyMaxLife * 0.6 && DamageReceived >= enemyMaxLife * 0.3)
            {
                DifficultyManager.Instance.AddFightResult(false, Fight.Mark.MEDIUM);
            }
            else if (DamageReceived < enemyMaxLife * 0.3)
            {
                DifficultyManager.Instance.AddFightResult(false, Fight.Mark.BAD);
            }
        }
    }
}
