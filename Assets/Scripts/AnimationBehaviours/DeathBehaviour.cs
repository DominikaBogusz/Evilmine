using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().MyRigidbody2D.velocity = Vector2.zero;
    }
}
