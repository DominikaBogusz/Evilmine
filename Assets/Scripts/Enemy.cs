using UnityEngine;

public class Enemy : Character {

    public EnemyAttributes Attributes { get; set; }
    public EnemyStatistics Statistics { get; set; }

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
            return Attributes.Health <= 0;
        }
    }

    public override void Start ()
    {
        base.Start();

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), Player.Instance.GetComponent<BoxCollider2D>(), true);

        Player.Instance.DeadEvent += new DeadEventHandler(RemoveTarget);

        ChangeState(new IdleState());

        Attributes = GetComponent<EnemyAttributes>();
        Attributes.Init();
        Attributes.AccomodateToDifficultyLevel(name);

        Statistics = GetComponent<EnemyStatistics>();
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
        else if(currentState is RangedState)
        {
            RemoveTarget();
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
            float xDist = Mathf.Abs(Target.transform.position.x - transform.position.x);

            if ((xDir < 0 && FacingRight || xDir > 0 && !FacingRight) && (xDist > meleeRange))
            {
                Flip();
            }
        }
        
    }

    public void SetTarget(GameObject target)
    {
        Target = target;

        Statistics.StartBattleTimer = true;
    }

    public void RemoveTarget()
    {
        if(Target != null)
        {
            Target = null;
            ChangeState(new IdleState());

            Statistics.StopBattleTimer = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(currentState != null)
        {
            currentState.OnTriggerEnter2D(other);
        }       
    }

    public void PlayerDamage(float damage)
    {
        SwordHide();

        if (!IsDead)
        {
            TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        Attributes.Health -= damage;
        Statistics.DamageReceived += damage;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            Statistics.StopBattleTimer = true;
            transform.GetChild(0).gameObject.SetActive(false);

            MyAnimator.SetTrigger("die");
            OnDeadEvent();
        }
    }
    
    public override void Die()
    {
        Destroy(gameObject);
        Player.Instance.Statistics.IncreaseKillCount();
    }
}
