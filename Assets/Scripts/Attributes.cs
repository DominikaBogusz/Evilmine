using UnityEngine;

public class Attributes : MonoBehaviour {

    [SerializeField] protected AttributesUI attributesUI;

    [SerializeField] public int initialHealth;

    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, initialHealth);
            attributesUI.UpdateHealth(health);
        }
    }

    [SerializeField] protected int minDamage;
    [SerializeField] protected int maxDamage;

    private int damage;
    public int Damage
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
