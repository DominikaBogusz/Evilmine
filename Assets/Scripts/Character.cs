using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    public Animator MyAnimator { get; private set; }
    public Rigidbody2D MyRigidbody2D { get; private set; }

    [SerializeField] protected float movementSpeed;

    public bool FacingRight { get; set; }
    public bool Attack { get; set; }
    public bool TakingDamage { get; set; }

    [SerializeField] protected int health;
    public abstract bool IsDead { get; }

    [SerializeField] private List<string> damageSources = new List<string>();

    [SerializeField] private PolygonCollider2D swordCollider;

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

    public abstract IEnumerator TakeDamage();

    public void SwordPullOut()
    {
        swordCollider.enabled = true;
    }

    public void SwordHide()
    {
        swordCollider.enabled = false;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }
}
