using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Character>().Attack = true;
        animator.SetFloat("speed", 0f);

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
        animator.GetComponent<Character>().Attack = false;
    }
}
