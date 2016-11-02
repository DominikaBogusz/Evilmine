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
    private Animator myAnimator;
    private bool facingRight;

    public Rigidbody2D MyRigidbody2D { get; set; }
    public bool Attack { get; set; }
    public bool Jump { get; set; }
    public bool Block { get; set; }
    public bool OnGround { get; set; }

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
        HandleMovement(horizontal);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Attack)
        {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.Space) && !Jump)
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
    }

    private void HandleMovement(float horizontalMove)
    {
        if ((OnGround || airControl) && !Attack)
        {
            MyRigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, MyRigidbody2D.velocity.y);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));

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
    }
}
