using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField] protected float movementSpeed;

    public bool FacingRight { get; set; }
    public bool Attack { get; set; }

    public virtual void Start()
    {
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
