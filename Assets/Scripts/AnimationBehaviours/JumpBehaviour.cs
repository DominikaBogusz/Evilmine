using UnityEngine;

public class JumpBehaviour : StateMachineBehaviour {
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("OnStateEnter, Jump=true");
        Player.Instance.Jump = true;
    }
}
