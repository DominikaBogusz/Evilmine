using UnityEngine;

public class Enemy : Character {

    public EnemyAttributes attributes { get; set; }

    private IEnemyState currentState;

    public GameObject Target { get; set; }

    public Transform LeftEdge { get; set; }
    public Transform RightEdge { get; set; }

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
            return attributes.Health <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();

        attributes = GetComponent<EnemyAttributes>();
        attributes.Init();

        Player.Instance.DeadEvent += new DeadEventHandler(RemoveTarget);
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
        if ((GetDirection().x > 0 && transform.position.x < RightEdge.position.x) || (GetDirection().x < 0 && transform.position.x > LeftEdge.position.x))
        {
            transform.Translate(GetDirection() * (MyAnimator.GetFloat("speed") * movementSpeed * Time.deltaTime), Space.World);
        }
        else if(currentState is PatrolState)
        {
            Flip();
        }
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

    public void RemoveTarget()
    {
        Target = null;
        ChangeState(new IdleState());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(currentState != null)
        {
            currentState.OnTriggerEnter2D(other);
        }       
    }

    public void PlayerDamage(int damage)
    {
        SwordHide();

        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        attributes.Health -= damage;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("hurt");
        }
        else
        {
            MyAnimator.SetTrigger("die");       
        }
    }
    
    public override void Die()
    {
        Destroy(gameObject);
    }
}
