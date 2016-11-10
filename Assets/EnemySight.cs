using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField] private Enemy enemy;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), other, true);
            enemy.Target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            enemy.Target = null;
        }
    }
}
