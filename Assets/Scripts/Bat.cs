using UnityEngine;
using System.Collections;

public class Bat : Character {

    [SerializeField] private float minHealth;
    [SerializeField] private float maxHealth;

    private float health;
    public float Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0f, maxHealth);
        }
    }

    public override bool IsDead
    {
        get
        {
            return Health <= 0;
        }
    }

    public Transform LeftEdge { get; set; }
    public Transform RightEdge { get; set; }

    public override void Start()
    {
        base.Start();
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);
        Health = Random.Range(minHealth, maxHealth);
    }

    void Update()
    {
        if (!IsDead && !TakingDamage)
        {
            Move();
        } 
    }

    public void Damage(float damage)
    {
        TakingDamage = true;
        Health -= damage;
        if (IsDead)
        {
            Die();
        }
        else
        {
            StartCoroutine(TakeDamage());
        }
    }


    private IEnumerator TakeDamage()
    {
        MyRigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        TakingDamage = false;
    }

    public override void Die()
    {
        MyAnimator.SetTrigger("die");
        GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.1f);
        GetComponent<BoxCollider2D>().offset = new Vector2(0f, -0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(DieCountDown());
    }

    private IEnumerator DieCountDown()
    {
        MyRigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    public void Move()
    {
        if ((GetDirection().x > 0 && transform.position.x < RightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > LeftEdge.position.x))
        {
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime), Space.World);
        }
        else
        {
            Flip();
        }
    }

    private Vector2 GetDirection()
    {
        return FacingRight ? Vector2.right : Vector2.left;
    }
}
