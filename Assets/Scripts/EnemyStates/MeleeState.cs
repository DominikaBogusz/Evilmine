using UnityEngine;

public class MeleeState : IEnemyState {

    private Enemy enemy;

    private float attackTimer;
    private float attackInterval;
    private bool canAttack = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        attackInterval = enemy.Attributes.AttackInterval;
    }

    public void Execute()
    {
        Attack();

        if (!enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        else if(enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    private void Attack()
    {
        enemy.MyAnimator.SetFloat("speed", 0f);
        enemy.MyAnimator.SetFloat("attackSpeed", enemy.Attributes.AttackSpeed);

        attackTimer += Time.deltaTime;

        if(attackTimer >= attackInterval)
        {
            canAttack = true;
            attackTimer = 0f;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
