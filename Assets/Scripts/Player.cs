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
    private Animator myAnimator;
    private bool facingRight;
    private IUseable useable;

    public Rigidbody2D MyRigidbody2D { get; set; }
    public bool Attack { get; set; }
    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool OnGround { get; set; }
    public bool OnLadder { get; set; }

    void Start ()
    {
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        HandleInput();
    }
	
	void FixedUpdate ()
    {
        OnGround = IsGrounded();
        HandleLayers();        

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
            myAnimator.SetTrigger("jump");
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
        if ((OnGround || airControl) && !Attack && !OnLadder)
        {
            MyRigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, MyRigidbody2D.velocity.y);
            myAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));
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

        if (OnGround && Jump)
        {
            Jump = false;
            MyRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }

        if (!OnGround && MyRigidbody2D.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }     

        if (horizontalMove > 0 && !facingRight || horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
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
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
        if (OnLadder)
        {
            myAnimator.SetLayerWeight(2, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(2, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Useable")
        {
            useable = other.GetComponent<IUseable>();
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Useable")
        {
            useable = null;
        }
    }

    void Use()
    {
        if(useable != null)
        {
            useable.Use();
        }
    }
}
