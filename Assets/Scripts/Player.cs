using UnityEngine;
using System.Collections;

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

    private Transform respawnPoint;

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
            return Attributes.Health.Get() <= 0;
        }
    }
    
    public bool Frozen { get; set; }

    public bool CanMove
    {
        get
        {
            return !TakingDamage && !IsDead && !Digging && !Frozen;
        }
    }

    public int MaxLivesCount { get; set; }
    public int ActiveLivesCount { get; set; }

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
        if (Died) return;
        SwordHide();

        if (!isImmortal)
        {
            Attributes.Health -= damage;

            if (!IsDead)
            {
                AnimationManager.Damage();
                StartCoroutine(TakeDamage());
            }
            else if (IsDead)
            {
                Died = true;
                AnimationManager.Die();
                OnDeadEvent();
            }
        }
    }

    public void EnemyDamage(Enemy enemy)
    {
        if (Died) return;
        SwordHide();

        int damage = Blocking ? enemy.Attributes.Damage.Get() - (enemy.Attributes.Damage.Get() * Attributes.ShieldProtection.Get()) / 100 : enemy.Attributes.Damage.Get();

        if (!isImmortal)
        {
            Attributes.Health -= damage;
            enemy.Statistics.DamageMade += damage;

            if (!IsDead)
            {
                if (Frozen)
                {
                    StartCoroutine(TakeDamage());
                }
                else if(OnGround && Blocking)
                {
                    AnimationManager.Protect();
                }
                else
                {
                    AnimationManager.Damage();
                    StartCoroutine(TakeDamage());
                }
            }
            else
            {
                Died = true;
                AnimationManager.Die();
                OnDeadEvent();
                enemy.Statistics.EvaluateFight();
                enemy.Statistics.IncreaseKillCount();
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
        DifficultyManager.Instance.PlayerProsperity -= 10;

        Attributes.Health.Set(Attributes.Health.max);
        transform.position = respawnPoint.position;

        AnimationManager.Reset();
        Died = false;
    }

    private void OnCollisionEnter2D(Collision2D info)
    {
        if (info.collider.tag == "Collectable")
        {
            info.collider.GetComponent<ICollectable>().Collect();
        }
    }

    public void SetRespawnPoint(Transform point)
    {
        respawnPoint = point;
    }

    public void Freeze(float time, Enemy enemy)
    {
        MyRigidbody2D.velocity = Vector2.zero;
        Frozen = true;
        MyAnimator.speed = 0f;
        StartCoroutine(StayFrozen(time));
        EnemyDamage(enemy);
    }

    private IEnumerator StayFrozen(float time)
    {
        yield return new WaitForSeconds(time);
        Frozen = false;
        MyAnimator.speed = 1f;
    }
}
