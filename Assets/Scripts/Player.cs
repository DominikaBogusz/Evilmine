using System.Collections;
using UnityEngine;

public delegate void DeadEventHandler();

public class Player : Character {

    [System.Serializable]
    public class PlayerStats : Stats
    { 
    }
    public PlayerStats stats = new PlayerStats();

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField] private Transform startPoint;   
    [SerializeField] private Transform[] groundPoints;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed;

    public PlayerAnimationManager AnimationManager { get; set; }
    public PlayerUseManager UseManager { get; set; }
    private SpriteRenderer spriteRenderer;

    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool Dig { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }
    public bool CanMove { get; set; }

    [SerializeField] private Collider2D shieldCollider;

    private bool isImmortal = false;
    [SerializeField] private float immortalTime = 1f;

    public override bool IsDead
    {
        get
        {
            return stats.CurHealth <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();
        stats.Init();
        AnimationManager = GetComponent<PlayerAnimationManager>();
        UseManager = GetComponentInChildren<PlayerUseManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CanMove = true;  
    }

    void Update()
    {
        OnGround = IsGrounded();
    }

    public void Move(float horizontalMove, float verticalMove)
    {
        if (!Attack && !OnLadder && CanMove)
        {
            MyRigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, MyRigidbody2D.velocity.y);
            AnimationManager.SetMovementSpeed(horizontalMove);

            if (horizontalMove > 0 && !FacingRight || horizontalMove < 0 && FacingRight)
            {
                Flip();
            }
        }
        
        if (OnLadder)
        { 
            MyRigidbody2D.velocity = new Vector2(0f, verticalMove * climbSpeed);
            if (OnGround && horizontalMove != 0)
            {
                OnLadder = false;
            } 
            if (OnGround)
            {
                AnimationManager.SetClimbSpeed(0f);
            }
            else
            {
                AnimationManager.SetClimbSpeed(verticalMove);
            }
        }

        if (!OnGround && MyRigidbody2D.velocity.y < 0)
        {
            AnimationManager.StartLand();
        }

        if (OnGround && Jump)
        {
            Jump = false;
            MyRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (Dig)
        {
            Dig = false;
            AnimationManager.Dig();
        }

        if (Block)
        {
            shieldCollider.enabled = true;
            GetComponent<BoxCollider2D>().size = new Vector2(0.2f, 1.55f);
        }
        else
        {
            shieldCollider.enabled = false;
            GetComponent<BoxCollider2D>().size = new Vector2(1f, 1.55f);
        }
    }

    private bool IsGrounded()
    {
        foreach(Transform point in groundPoints)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

            for(int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject != gameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            TakeEnemyDamage(10);
        }
        else if (other.tag == "Thorn")
        {
            TakeEnvironmentDamage(10);
        }
    }

    public void TakeEnemyDamage(int damage)
    {
        SwordHide();

        if (!isImmortal && !Block)
        {
            StartCoroutine(TakeDamage(damage));
        }
    }

    public void TakeEnvironmentDamage(int damage)
    {
        SwordHide();

        if (!isImmortal)
        {
            StartCoroutine(TakeDamage(damage));
        }
    }

    public override IEnumerator TakeDamage(int damage)
    {
        stats.CurHealth -= damage;

        if (!IsDead && OnGround)
        {
            AnimationManager.Hurt();

            isImmortal = true;
            StartCoroutine(IndicateImmortal());
            yield return new WaitForSeconds(immortalTime);
            isImmortal = false;
        }
        else if (IsDead)
        {
            OnDeadEvent();
            AnimationManager.Die();
        }
    }

    private IEnumerator IndicateImmortal()
    {
        while (isImmortal)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public event DeadEventHandler DeadEvent;

    public void OnDeadEvent()
    {
        if(DeadEvent != null)
        {
            DeadEvent();
        }
    }

    public override void Die()
    {
        stats.Init();
        transform.position = startPoint.position;
    }
}
