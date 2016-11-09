using UnityEngine;

public class Enemy : Character {

    private IEnemyState currentState;

	public override void Start ()
    {
        base.Start();
        ChangeState(new IdleState());
	}
	
	void Update ()
    {
        currentState.Execute();
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
        MyAnimator.SetFloat("speed", 1f);
        transform.Translate(GetDirection() * movementSpeed * Time.deltaTime);
    }

    private Vector2 GetDirection()
    {
        return FacingRight ? Vector2.right : Vector2.left;
    }
}
