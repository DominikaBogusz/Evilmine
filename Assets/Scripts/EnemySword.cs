using UnityEngine;

public class EnemySword : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player.Instance.EnemyDamage(GetComponentInParent<Enemy>());
        }
    }
}
