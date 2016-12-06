using UnityEngine;

public class DamageBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().TakingDamage = true;

        if (animator.tag == "Player")
        {
            Player.Instance.MyRigidbody2D.velocity = new Vector2(0f, Player.Instance.MyRigidbody2D.velocity.y);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().TakingDamage = false;
    }
}
