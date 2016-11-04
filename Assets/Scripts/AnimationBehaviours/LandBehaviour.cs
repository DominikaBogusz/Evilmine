using UnityEngine;

public class LandBehaviour : StateMachineBehaviour {

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.Instance.OnGround)
        {
            animator.SetBool("land", false);
        }
    }
}
