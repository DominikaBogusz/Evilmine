using UnityEngine;

public class ClimbBehaviour : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("climbSpeed", 0f);
	}
}
