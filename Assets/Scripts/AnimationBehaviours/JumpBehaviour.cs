using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour {
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Jump = true;
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Jump = false;
    }
}
