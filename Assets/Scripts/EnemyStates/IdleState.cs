﻿using UnityEngine;

public class IdleState : IEnemyState {

    private Enemy enemy;

    private float idleTimer;
    private float idleDuration = 2f;

    public void Enter(Enemy enemy)
    {
        Debug.Log("Idle enter");
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

        idleTimer += Time.deltaTime;

        if(idleTimer >= idleDuration)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
}