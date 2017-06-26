using UnityEngine;

public class DigBehaviour : StateMachineBehaviour {

    private Gem gem;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gem = Player.Instance.UseManager.Useable as Gem;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.Digging = false;
        gem.GenerateStuff();
    }
}
