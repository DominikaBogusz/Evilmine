using UnityEngine;

public class PlayerSword : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.PlayerDamage(Player.Instance.Attributes.Damage);
            }
        }
    }
}
