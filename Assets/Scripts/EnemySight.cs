using UnityEngine;

public class EnemySight : MonoBehaviour {

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), other, true);     
            enemy.SetTarget(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.RemoveTarget();
        }
    }
}
