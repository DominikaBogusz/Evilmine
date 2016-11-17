using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().Attacking = true;
        animator.SetFloat("speed", 0f);
        animator.GetComponent<Character>().MyRigidbody2D.velocity = Vector2.zero;      
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().Attacking = false;
    }
}
