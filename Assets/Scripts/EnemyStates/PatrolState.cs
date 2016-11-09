using UnityEngine;

public class PatrolState : IEnemyState {

    private Enemy enemy;

    private float patrolTimer;
    private float patrolDuration = 10f;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void Patrol()
    {
        enemy.MyAnimator.SetFloat("speed", 1f);

        patrolTimer += Time.deltaTime;
        if (patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
