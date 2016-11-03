using UnityEngine;

public class LandBehaviour : StateMachineBehaviour {

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.Instance.OnGround)
        {
            Debug.Log("OnStateUpdate, land=false");
            animator.SetBool("land", false);
        }
    }
}
