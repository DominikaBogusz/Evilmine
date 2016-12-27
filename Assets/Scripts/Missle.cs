using UnityEngine;

public class Missle : MonoBehaviour {

    private Enemy enemy;

    [SerializeField] private float speed;
    [SerializeField] private float freezeTime;

    private Rigidbody2D myRigidbody;
    private Vector2 direction;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        myRigidbody.velocity = direction * speed;
    }

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        direction = enemy.GetDirection();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            speed = 0f;
            GetComponent<Collider2D>().enabled = false;

            if (freezeTime == 0)
            {
                Player.Instance.EnemyDamage(enemy);
            }
            else
            {
                if (!Player.Instance.Frozen)
                {
                    Player.Instance.Freeze(freezeTime, enemy);
                    Destroy(gameObject, freezeTime);
                    return;
                }
            }
            Destroy(gameObject);
        }  
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
