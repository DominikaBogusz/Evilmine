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
            GetComponent<BoxCollider2D>().size = new Vector2(9f, 4f);
            enemy.SetTarget(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<BoxCollider2D>().size = new Vector2(9f, 0.5f);
            
            if(enemy.Target != null)
            {
                enemy.RemoveTarget();
            }  
        }
    }
}
