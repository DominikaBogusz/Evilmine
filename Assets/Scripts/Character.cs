using UnityEngine;

public abstract class Character : MonoBehaviour {

    public Animator MyAnimator { get; private set; }
    public Rigidbody2D MyRigidbody2D { get; private set; }

    [SerializeField] protected float movementSpeed;

    public bool FacingRight { get; set; }
    public bool Attack { get; set; }

    public virtual void Start()
    {
        MyAnimator = GetComponent<Animator>();
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        FacingRight = true;
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
