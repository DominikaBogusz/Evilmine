using UnityEngine;
using System.Collections;

public class LandBehavior : StateMachineBehaviour {

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.Instance.OnGround)
        {
            animator.SetBool("land", false);
            //animator.ResetTrigger("jump");
        }
    }
}
