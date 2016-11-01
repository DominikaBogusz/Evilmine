using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Attack = true;

        if (Player.Instance.OnGround)
        {
            Player.Instance.MyRigidbody2D.velocity = Vector2.zero;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Attack = false;
        //animator.ResetTrigger("attack");
    }
}
