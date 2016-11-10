using UnityEngine;

public class PatrolState : IEnemyState {

    private Enemy enemy;

    private float patrolTimer;
    private float patrolDuration = 5f;

    public void Enter(Enemy enemy)
    {
        Debug.Log("Patrol enter");
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();

        if (!enemy.Attack)
        {
            enemy.Move();
        }
        
        if(enemy.Target != null)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Edge")
        {
            enemy.Flip();
        }
    }

    private void Patrol()
    {
        enemy.MyAnimator.SetFloat("speed", 0.5f);

        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
