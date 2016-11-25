using UnityEngine;

public class EnemyAttributes : Attributes {

    private EnemyAttributesUI enemyAttributesUI;

    void Awake()
    {
        enemyAttributesUI = attributesUI as EnemyAttributesUI;
    }

    [SerializeField] private int minAttackInterval;
    [SerializeField] private int maxAttackInterval;

    private int attackInterval;
    public int AttackInterval
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
        if (DifficultyManager.Instance.EnemiesDifficultyLevel.ContainsKey(enemyName))
        {
            float floatLevel = DifficultyManager.Instance.EnemiesDifficultyLevel[enemyName];
            int intLevel = (int)(floatLevel * 100);
            Damage = ((minDamage + maxDamage) / 2 * intLevel) / 100;
            AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2 * floatLevel;
        }
    }
}
