using UnityEngine;

public class HurtBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().TakingDamage = true;

        if(animator.tag == "Player")
        {
            if (Player.Instance.OnGround)
            {
                Player.Instance.MyRigidbody2D.velocity = Vector2.zero;
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().TakingDamage = false;
    }
}
