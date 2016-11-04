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

    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float climbSpeed = 5;
    
    public IUseable Useable { get; set; }

    public bool FacingRight { get; set; }

    private Animator myAnimator;
    private enum animLayer { GROUND, AIR, LADDER };
    private animLayer currentAnimLayer;

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
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        FacingRight = true;
        currentAnimLayer = animLayer.GROUND;
        CanMove = true;  
    }

    void Update()
    {
        OnGround = IsGrounded();
        HandleLayers();

        HandleInput();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        HandleMovement(horizontal, vertical);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Attack)
        {
            myAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.Space) && !Jump && !OnLadder)
        {
            myAnimator.SetBool("jump", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !Block)
        {
            myAnimator.SetBool("block", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            myAnimator.SetBool("block", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
    }

    private void HandleMovement(float horizontalMove, float verticalMove)
    {
        if ((OnGround || airControl) && !Attack && !OnLadder && CanMove)
        {
            MyRigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, MyRigidbody2D.velocity.y);
            myAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));

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
                myAnimator.SetFloat("climbSpeed", 0f);
            }
            else
            {
                myAnimator.SetFloat("climbSpeed", Mathf.Abs(verticalMove));
            }
        }

        if (!OnGround && MyRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }

        if (OnGround && Jump)
        {
            Jump = false;
            MyRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (Dig)
        {
            Dig = false;
            myAnimator.SetTrigger("dig");
        }
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; 
        transform.localScale = theScale;  
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

    private void HandleLayers()
    {
        switch (currentAnimLayer)
        {
            case animLayer.GROUND:
                if (!OnGround && !OnLadder)
                {
                    SwitchToLayer(animLayer.AIR);
                }
                if (OnLadder)
                {
                    SwitchToLayer(animLayer.LADDER);
                }
                break;
            case animLayer.AIR:
                if (OnGround && !OnLadder)
                {
                    myAnimator.SetBool("jump", false);
                    SwitchToLayer(animLayer.GROUND);
                }
                if (OnLadder)
                {
                    SwitchToLayer(animLayer.LADDER);
                }
                break;
            case animLayer.LADDER:
                if (!OnLadder)
                {
                    SwitchToLayer(animLayer.GROUND);
                }
                break;
        }
    }

    private void SwitchToLayer(animLayer nextAnimLayer)
    {
        myAnimator.SetLayerWeight((int)currentAnimLayer, 0);
        myAnimator.SetLayerWeight((int)nextAnimLayer, 1);
        currentAnimLayer = nextAnimLayer;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Useable")
        {
            Useable = other.GetComponent<IUseable>();
        } 
    }

    void Use()
    {
        if(Useable != null)
        {
            Useable.Use();
        }
    }
}
