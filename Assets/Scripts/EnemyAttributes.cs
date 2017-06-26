using UnityEngine;

public class EnemyAttributes : MonoBehaviour {

    [SerializeField] public Attribute Level;
    [SerializeField] public Attribute Health;
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute AttackInterval;

    public void Init()
    {
        Level.Set(DifficultyManager.Instance.ExpectedEnemyLevel);

        int steps = Level.max - Level.min + 1;

        int healthChange = (Health.max * 2) / steps;
        Health.max = (Level.Get() - Level.min + 1) * healthChange;
        Health.Set(Health.max);

        float damage = Damage.min + (AttributeChange(Damage, steps) * Level.Get());
        Damage.Set((int)damage);

        float attackSpeed = AttackSpeed.min + (AttributeChange(AttackSpeed, steps) * Level.Get());
        AttackSpeed.Set((int)attackSpeed);

        float attackInterval = AttackInterval.min + (AttributeChange(AttackInterval, steps) * ((Level.min + Level.max) - Level.Get()));
        AttackInterval.Set((int)attackInterval);
    }

    float AttributeChange(Attribute attribute, int numberOfSteps)
    {
        return (float) (attribute.max - attribute.min) / numberOfSteps;
    }
}
