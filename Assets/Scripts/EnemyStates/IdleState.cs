using UnityEngine;

public class IdleState : IEnemyState {

    private Enemy enemy;

    private float idleTimer;
    private float idleDuration;

    public void Enter(Enemy enemy)
    {
        idleDuration = Random.Range(1f, 3f);
        this.enemy = enemy;
    }

    public void Execute()
    {
        Idle();

        if(enemy.Target != null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void Idle()
    {
        enemy.MyAnimator.SetFloat("speed", 0f);
        enemy.MyRigidbody2D.velocity = new Vector2(0f, enemy.MyRigidbody2D.velocity.y);

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}
