using UnityEngine;
using System.Collections;

public delegate void DeadEventHandler();

public class Player : Character {

    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
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

    public PlayerAttributes Attributes { get; set; }
    public PlayerAnimationManager AnimationManager { get; set; }
    public PlayerUseManager UseManager { get; set; }
    public PlayerStatistics Statistics { get; set; }
    private SpriteRenderer spriteRenderer;

    public bool Jumping { get; set; }
    public bool Blocking { get; set; }
    public bool Digging { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }

    [SerializeField] private Collider2D shieldCollider;

    private bool isImmortal = false;
    [SerializeField] private float immortalTime = 1f;

    public override bool IsDead
    {
        get
        {
            return Attributes.Health <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();

        Attributes = GetComponent<PlayerAttributes>();
        Attributes.Init();

        AnimationManager = GetComponent<PlayerAnimationManager>();
        UseManager = GetComponentInChildren<PlayerUseManager>();
        Statistics = GetComponent<PlayerStatistics>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnGround = IsGrounded();
    }

    public void Move(float horizontalMove, float verticalMove)
    {
        if (!Attacking && !OnLadder)
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

        if (OnGround && Jumping)
        {
            Jumping = false;
            MyRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (Digging)
        {
            AnimationManager.Dig();
        }

        if (Blocking)
        {
            shieldCollider.enabled = true;
        }
        else
        {
            shieldCollider.enabled = false;
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

    public void EnvironmentDamage(int damage)
    {
        SwordHide();

        if (!isImmortal)
        {
            Attributes.Health -= damage;

            if (!IsDead && OnGround)
            {
                AnimationManager.Damage();
                StartCoroutine(TakeDamage());
            }
            else if (IsDead)
            {
                AnimationManager.Die();
                OnDeadEvent();
            }
        }
    }

    public void EnemyDamage(Enemy enemy)
    {
        SwordHide();

        float damage = Blocking ? enemy.Attributes.Damage - (enemy.Attributes.Damage * Attributes.ShieldProtectionPercent) / 100 : enemy.Attributes.Damage;

        if (!isImmortal) 
        {
            Attributes.Health -= damage;
            enemy.Statistics.DamageMade += damage;

            if (!IsDead && OnGround && Blocking)
            {
                AnimationManager.Protect();
            }
            else if (!IsDead && OnGround)
            {
                AnimationManager.Damage();
                StartCoroutine(TakeDamage());
            }
            else if (IsDead)
            {
                AnimationManager.Die();
                OnDeadEvent();
                enemy.Statistics.KillCount++;
            }
        }
    }

    private IEnumerator TakeDamage()
    {
        isImmortal = true;
        StartCoroutine(IndicateImmortal());
        yield return new WaitForSeconds(immortalTime);
        isImmortal = false;
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

    public override void Die()
    {
        Attributes.Init();
        transform.position = startPoint.position;

        AnimationManager.Reset();
    }

    public event DeadEventHandler DeadEvent;

    public void OnDeadEvent()
    {
        if (DeadEvent != null)
        {
            DeadEvent();
        }
    }
}
