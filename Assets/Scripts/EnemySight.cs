using UnityEngine;

public class EnemySight : MonoBehaviour {

    [SerializeField] private Enemy enemy;

    [SerializeField] private Collider2D sword;

    void Start()
    {
        //TODO: bez umieszczania miecza przez inspektora, dla wszystkich broni
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), sword, true);
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Physics2D.IgnoreCollision(enemy.GetComponent<Collider2D>(), other, true);     
            enemy.Target = other.gameObject;
        }
        if(other.tag == "Sword")
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other, true);
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
