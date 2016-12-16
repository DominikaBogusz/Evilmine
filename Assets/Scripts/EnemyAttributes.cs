using UnityEngine;

public class EnemyAttributes : Attributes {

    private EnemyAttributesUI enemyAttributesUI;

    void Awake()
    {
        enemyAttributesUI = attributesUI as EnemyAttributesUI;
    }

    [SerializeField] private float minAttackInterval;
    [SerializeField] private float maxAttackInterval;

    private float attackInterval;
    public float AttackInterval
    {
        get { return attackInterval; }
        set
        {
            attackInterval = Mathf.Clamp(value, minAttackInterval, maxAttackInterval);
            enemyAttributesUI.UpdateAttackInterval(attackInterval);
        }
    }

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            enemyAttributesUI.UpdateLevel(level);
        }
    }

    public override void Init()
    {
        base.Init();
        Damage = (minDamage + maxDamage) / 2;
        AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2;
        AttackInterval = (minAttackInterval + maxAttackInterval) / 2;
        Level = Player.Instance.Attributes.Level;
        if(Level > 1)
        {
            initialHealth = initialHealth + initialHealth * Level / 10;
        }
    }

    public void AccomodateToDifficultyLevel(string enemyName)
    {
        if (DifficultyManager.Instance.EnemyTypesDifficulty.ContainsKey(enemyName))
        {
            float level = DifficultyManager.Instance.EnemyTypesDifficulty[enemyName];
            Damage = (minDamage + maxDamage) / 2 * level;
            AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2 * level;
            AttackInterval = (minAttackInterval + maxAttackInterval) / 2 * (1-(level-1));
        }
    }
}
