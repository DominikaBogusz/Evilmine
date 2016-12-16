using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour {

    public AttributeInt Level { get; set; }
    [SerializeField] private Text levelText;

    public AttributeInt Health { get; set; }
    [SerializeField] private Text healthText;
    [SerializeField] private StatusIndicatorUI healthIndicator;

    public AttributeInt Damage { get; set; }
    [SerializeField] private Text damageText;

    public AttributeFloat AttackSpeed { get; set; }
    [SerializeField] private Text attackSpeedText;

    public AttributeFloat AttackInterval { get; set; }
    [SerializeField] private Text attackIntervalText;

    void Awake()
    {
        Level = new AttributeInt("Level", 1, 100, levelText);
        Health = new AttributeInt("Health", 0, 100, healthText, healthIndicator);
        Damage = new AttributeInt("Damage", 5, 20, damageText);
        AttackSpeed = new AttributeFloat("AttackSpeed", 0.8f, 1.4f, attackSpeedText);
        AttackInterval = new AttributeFloat("AttackInterval", 1f, 3f, attackIntervalText);
    }

    public void Init()
    {
        Level.Set(1);
        Health.Set(100);
        Damage.Set(10);
        AttackSpeed.Set(1f);
        AttackInterval.Set(80);
    } 

    public void AccomodateToDifficultyLevel(string enemyName)
    {
        if (DifficultyManager.Instance.EnemyTypesDifficulty.ContainsKey(enemyName))
        {
            float level = DifficultyManager.Instance.EnemyTypesDifficulty[enemyName];

            //TODO jakieś gówno wszystko źle

            //Damage = (minDamage + maxDamage) / 2 * level;
            //AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2 * level;
            //AttackInterval = (minAttackInterval + maxAttackInterval) / 2 * (1-(level-1));
        }
    }
}
