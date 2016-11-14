using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour {

    float respawnTime = 3f;
    float deathTimer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().MyRigidbody2D.velocity = Vector2.zero;

        deathTimer = 0f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deathTimer += Time.deltaTime;

        if (deathTimer >= respawnTime)
        {
            animator.ResetTrigger("die");
            animator.GetComponent<Character>().Die();
            animator.Play("Idle");
        }
    }
}
