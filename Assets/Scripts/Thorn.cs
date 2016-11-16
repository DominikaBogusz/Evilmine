using UnityEngine;

public class Thorn : MonoBehaviour {

    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = Mathf.Clamp(value, minDamage, maxDamage); }
    }

    void Start()
    {
        Damage = (minDamage + maxDamage) / 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.EnvironmentDamage(Damage);
        }
    }
}
