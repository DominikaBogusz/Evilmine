using UnityEngine;
using System.Collections;

public class Enemy : Character {

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    [SerializeField] private float meleeRange = 1f;

    public bool InMeleeRange
    {
        get
        {
            if(Target != null)
            {
                float distance = Vector2.Distance(transform.position, Target.transform.position);
                return distance <= meleeRange;
            }
            return false;
        }
    }

    public override bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
	}
	
	void Update ()
    {
        if (!IsDead && !TakingDamage)
        {
            currentState.Execute();
            LookAtTarget();
        }
	}

    public void ChangeState(IEnemyState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime), Space.World);
    }

    private Vector2 GetDirection()
    { 
        return FacingRight ? Vector2.right : Vector2.left;
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if(xDir < 0 && FacingRight || xDir > 0 && !FacingRight)
            {
                Flip();
            }
        }
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter2D(other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("hurt");
        }
        else
        {
            MyAnimator.SetTrigger("die");
            yield return null;
        }
    }
}
