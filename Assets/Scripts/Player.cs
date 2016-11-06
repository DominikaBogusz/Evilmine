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
    [SerializeField] private bool airControl;
    [SerializeField] private float jumpForce;
    [SerializeField] private float climbSpeed = 5;

    public bool FacingRight { get; set; }

    public PlayerAnimationController AnimationController { get; set; }
    public PlayerUseManager UseManager { get; set; }

    public Rigidbody2D MyRigidbody2D { get; set; }
    public bool Attack { get; set; }
    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool Dig { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }
    public bool CanMove { get; set; }

    void Start ()
    {
        AnimationController = GetComponent<PlayerAnimationController>();
        UseManager = GetComponentInChildren<PlayerUseManager>();
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        FacingRight = true;
        CanMove = true;  
    }

    void Update()
    {
        OnGround = IsGrounded();

        HandleInput();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Attack)
        {
            AnimationController.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !Jump && !OnLadder)
        {
            AnimationController.StartJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !Block)
        {
            AnimationController.StartBlock();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            AnimationController.StopBlock();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseManager.Use();
        }
    }

    private void HandleMovement(float horizontalMove, float verticalMove)
    {
        if ((OnGround || airControl) && !Attack && !OnLadder && CanMove)
        {
            MyRigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, MyRigidbody2D.velocity.y);
            AnimationController.SetMovementSpeed(horizontalMove);

            if (horizontalMove > 0 && !FacingRight || horizontalMove < 0 && FacingRight)
            {
                AnimationController.Flip();
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
                AnimationController.SetClimbSpeed(0f);
            }
            else
            {
                AnimationController.SetClimbSpeed(verticalMove);
            }
        }

        if (!OnGround && MyRigidbody2D.velocity.y < 0)
        {
            AnimationController.StartLand();
        }

        if (OnGround && Jump)
        {
            Jump = false;
            MyRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (Dig)
        {
            Dig = false;
            AnimationController.Dig();
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
