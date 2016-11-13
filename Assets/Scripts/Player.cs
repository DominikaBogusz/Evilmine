using System.Collections;
using UnityEngine;

public delegate void DeadEventHandler();

public class Player : Character {

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
    [SerializeField] private float climbSpeed = 5;

    public PlayerAnimationManager AnimationManager { get; set; }
    public PlayerUseManager UseManager { get; set; }
    private SpriteRenderer spriteRenderer;

    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool Dig { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }
    public bool CanMove { get; set; }

    private bool isImmortal = false;
    [SerializeField] private float immortalTime = 1f;

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();
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

    public override IEnumerator TakeDamage()
    {
        if (!isImmortal)
        {
            health -= 10;

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
        health = 50;
        transform.position = startPoint.position;
    }
}
