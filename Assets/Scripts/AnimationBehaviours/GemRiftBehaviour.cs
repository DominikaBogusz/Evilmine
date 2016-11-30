using UnityEngine;

public class GemRiftBehaviour : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), false);
    }
}
