using UnityEngine;

public delegate void DeadEventHandler();

public abstract class Character : MonoBehaviour {

    public Animator MyAnimator { get; private set; }
    public Rigidbody2D MyRigidbody2D { get; private set; }

    [SerializeField] protected float movementSpeed;
    
    public bool FacingRight { get; set; }
    public bool Attacking { get; set; }
    public bool TakingDamage { get; set; }
    public abstract bool IsDead { get; }
    public bool Died { get; set; }

    [SerializeField] private Collider2D swordCollider;

    [SerializeField] private Transform[] dontFlipUI;

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

        for (int i = 0; i < dontFlipUI.Length; i++)
        {
            Vector3 scale = dontFlipUI[i].localScale;
            scale.x *= -1;
            dontFlipUI[i].localScale = scale;
        }
    }    

    public void SwordPullOut()
    {
        swordCollider.enabled = true;

        Vector3 tmpPos = swordCollider.transform.position;
        swordCollider.transform.position = new Vector3(swordCollider.transform.position.x + 0, 01, swordCollider.transform.position.y);
        swordCollider.transform.position = tmpPos;
    }

    public void SwordHide()
    {
        if(swordCollider != null)
        {
            swordCollider.enabled = false;
        }
    }

    public event DeadEventHandler DeadEvent;

    public void OnDeadEvent()
    {
        if (DeadEvent != null)
        {
            DeadEvent();
        }
    }

    public abstract void Die();
}
