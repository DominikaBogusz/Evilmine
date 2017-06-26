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

    public override void Init()
    {
        base.Init();
        AttackInterval = (minAttackInterval + maxAttackInterval) / 2;
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
