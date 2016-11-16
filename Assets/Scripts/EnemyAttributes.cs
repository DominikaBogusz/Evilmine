using UnityEngine;

public class EnemyAttributes : Attributes {

    [SerializeField] private int minAttackInterval;
    [SerializeField] private int maxAttackInterval;

    private int attackInterval;
    public int AttackInterval
    {
        get { return attackInterval; }
        set { attackInterval = Mathf.Clamp(value, minAttackInterval, maxAttackInterval); }
    }

    public override void Init()
    {
        base.Init();
        attackInterval = (minAttackInterval + maxAttackInterval) / 2;
    }
}
