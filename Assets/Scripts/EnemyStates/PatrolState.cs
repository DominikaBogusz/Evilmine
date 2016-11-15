using UnityEngine;

public class PatrolState : IEnemyState {

    private Enemy enemy;

    private float patrolTimer;
    private float patrolDuration;

    public void Enter(Enemy enemy)
    {
        patrolDuration = Random.Range(1f, 5f);
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();

        if (!enemy.Attacking)
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
