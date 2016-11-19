using UnityEngine;

public class EnemyAttributes : Attributes {

    private EnemyAttributesUI enemyAttributesUI;

    void Start()
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
        attackInterval = (minAttackInterval + maxAttackInterval) / 2;
    }
}
