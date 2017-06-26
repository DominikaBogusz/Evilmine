using UnityEngine;

public class EnemyAttributes : MonoBehaviour {

    [SerializeField] private StatusIndicatorUI healthStatusIndicator;

    [HideInInspector] public Attribute Level;
    [SerializeField] public Attribute Health;
    [SerializeField] private int initialMaxHealth;
    public int ActualMaxHealth { get; set; }
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute AttackInterval;

    public void Init(int minLevel, int maxLevel)
    {
        Level.min = minLevel;
        Level.max = maxLevel;

        Level.Set(DifficultyManager.Instance.ExpectedEnemyLevel);

        int steps = Level.max - Level.min + 1;

        float health = initialMaxHealth + (Change(initialMaxHealth, Health.max, steps) * Level.Get());
        ActualMaxHealth = (int)health;
        Health.Set(ActualMaxHealth);

        float damage = Damage.min + (Change(Damage.min, Damage.max, steps) * Level.Get());
        Damage.Set((int)damage);

        float attackSpeed = AttackSpeed.min + (Change(AttackSpeed.min, AttackSpeed.max, steps) * Level.Get());
        AttackSpeed.Set((int)attackSpeed);

        float attackInterval = AttackInterval.min + (Change(AttackInterval.min, AttackInterval.max, steps) * ((Level.min + Level.max) - Level.Get()));
        AttackInterval.Set((int)attackInterval);

        Health.ChangeEvent += new ChangeEventHandler(UpdateHealthStatusBar);
    }

    float Change(int attrMin, int attrMax, int numberOfSteps)
    {
        return (float) (attrMax - attrMin) / numberOfSteps;
    }

    void UpdateHealthStatusBar()
    {
        healthStatusIndicator.SetStatusBar(Health.Get(), ActualMaxHealth);
    }
}
