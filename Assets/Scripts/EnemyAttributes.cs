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
}
