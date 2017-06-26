using UnityEngine;

public class Player : MonoBehaviour {

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

    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private Transform[] groundPoints;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed = 5;

    public Rigidbody2D MyRigidbody2D { get; set; }
    
    public PlayerAnimationManager AnimationManager { get; set; }
    public PlayerUseManager UseManager { get; set; }

    public bool FacingRight { get; set; }
    public bool Attack { get; set; }
    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool Dig { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }
    public bool CanMove { get; set; }

    void Start ()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        AnimationManager = GetComponent<PlayerAnimationManager>();
        UseManager = GetComponentInChildren<PlayerUseManager>();
        FacingRight = true;
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
                AnimationManager.Flip();
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
}
