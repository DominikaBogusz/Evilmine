using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rigidbody2D;

    private Animator animator;

    private bool facingRight;

    [SerializeField]
    private float movementSpeed = 5;

    private bool isAttacking;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;

    private bool isJumping;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    private bool isJumpAttacking;

	void Start ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        HandleInput();
    }
	
	void FixedUpdate ()
    {
        isGrounded = IsGrounded();
        HandleLayers();

        float horizontal = Input.GetAxis("Horizontal");
        HandleMovement(horizontal);

        HandleAttacks();

        ResetValues();
	}

    private void HandleMovement(float horizontalMove)
    {
        if(rigidbody2D.velocity.y < 0)
        {
            animator.SetBool("land", true);
        }

        if(isGrounded && isJumping)
        {
            animator.SetTrigger("jump");
            isGrounded = false;
            rigidbody2D.AddForce(new Vector2(0, jumpForce * 10));
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl))
        {
            rigidbody2D.velocity = new Vector2(horizontalMove * movementSpeed, rigidbody2D.velocity.y);
        }
        
        animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        if (horizontalMove > 0 && !facingRight || horizontalMove < 0 && facingRight)
        {
            Flip();
        }
    }

    private void HandleAttacks()
    {
        if (isAttacking && isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            animator.SetTrigger("attack");
            rigidbody2D.velocity = Vector2.zero;
        }

        if(isJumpAttacking && !isGrounded && !animator.GetCurrentAnimatorStateInfo(1).IsTag("JumpAttack"))
        {
            animator.SetBool("jumpAttack", true);
        }
        //if (!isJumpAttacking && !animator.GetCurrentAnimatorStateInfo(1).IsTag("JumpAttack"))
        //{
           // animator.SetBool("jumpAttack", false);
        //}
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isAttacking = true;
            isJumpAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;  
    }

    private void ResetValues()
    {
        isAttacking = false;
        isJumping = false;
        isJumpAttacking = false;
    }

    private bool IsGrounded()
    {
        //if(rigidbody2D.velocity.y <= 0)
        //{
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for(int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        animator.SetBool("land", false);
                        animator.SetBool("jumpAttack", false);
                        return true;
                    }
                }
            }
        //}
        return false;
    }

    private void HandleLayers()
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }
}
