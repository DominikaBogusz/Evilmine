using UnityEngine;

public class PlayerSword : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponentInParent<Enemy>().PlayerDamage(Player.Instance.Attributes.Damage);
        }
        else if(other.tag == "Bat")
        {
            other.GetComponent<Bat>().Damage(Player.Instance.Attributes.Damage);
        }
        else if(other.tag == "Box")
        {
            other.GetComponent<Box>().GenerateStuff();
        }
    }
}
