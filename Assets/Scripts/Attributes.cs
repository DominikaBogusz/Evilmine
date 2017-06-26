using UnityEngine;

public class Attributes : MonoBehaviour {

    [SerializeField] protected AttributesUI attributesUI;
    [SerializeField] private HealthIndicatorUI healthIndicatorUI;

    [SerializeField] public float initialHealth;

    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0f, initialHealth);
            healthIndicatorUI.SetHealth(health, initialHealth);
            attributesUI.UpdateHealth(health);
        }
    }

    [SerializeField] protected float minDamage;
    [SerializeField] protected float maxDamage;

    private float damage;
    public float Damage
    {
        get { return damage; }
        set
        {
            damage = Mathf.Clamp(value, minDamage, maxDamage);
            attributesUI.UpdateDamage(damage);
        }
    }

    [SerializeField] protected float minAttackSpeed;
    [SerializeField] protected float maxAttackSpeed;

    private float attackSpeed;
    public float AttackSpeed
    {
        get { return attackSpeed; }
        set
        {
            attackSpeed = Mathf.Clamp(value, minAttackSpeed, maxAttackSpeed);
            attributesUI.UpdateAttackSpeed(attackSpeed);
        }
    }

    public virtual void Init()
    {
        Health = initialHealth;
        Damage = (minDamage + maxDamage) / 2;
        AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2;
    }
}
