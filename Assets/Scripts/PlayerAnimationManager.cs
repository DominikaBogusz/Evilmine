using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

    private Player player;
    
    private enum animLayer { GROUND, AIR, LADDER };
    private animLayer currentAnimLayer;

    void Start ()
    {
        player = Player.Instance;
        currentAnimLayer = animLayer.GROUND;
    }
	
	void Update ()
    {
        HandleLayers();
	}

    public void HandleLayers()
    {
        switch (currentAnimLayer)
        {
            case animLayer.GROUND:
                if (!player.OnGround && !player.OnLadder)
                {
                    SwitchToLayer(animLayer.AIR);
                }
                if (player.OnLadder)
                {
                    SwitchToLayer(animLayer.LADDER);
                }
                break;
            case animLayer.AIR:
                if (player.OnGround && !player.OnLadder)
                {
                    player.MyAnimator.SetBool("jump", false);
                    SwitchToLayer(animLayer.GROUND);
                }
                if (player.OnLadder)
                {
                    SwitchToLayer(animLayer.LADDER);
                }
                break;
            case animLayer.LADDER:
                if (!player.OnLadder)
                {
                    SwitchToLayer(animLayer.GROUND);
                }
                break;
        }
    }

    private void SwitchToLayer(animLayer nextAnimLayer)
    {
        player.MyAnimator.SetLayerWeight((int)currentAnimLayer, 0);
        player.MyAnimator.SetLayerWeight((int)nextAnimLayer, 1);
        currentAnimLayer = nextAnimLayer;
    }

    public void SetMovementSpeed(float speed)
    {
        player.MyAnimator.SetFloat("speed", Mathf.Abs(speed));
    }
    public void SetClimbSpeed(float speed)
    {
        player.MyAnimator.SetFloat("climbSpeed", speed);
    }

    public void Attack()
    {
        player.MyAnimator.SetTrigger("attack");
    }
    public void StartBlock()
    {
        player.MyAnimator.SetBool("block", true);
    }
    public void StopBlock()
    {
        player.MyAnimator.SetBool("block", false);
    }
    public void StartJump()
    {
        player.MyAnimator.SetBool("jump", true);
    }
    public void StopJump()
    {
        player.MyAnimator.SetBool("jump", false);
    }
    public void StartLand()
    {
        player.MyAnimator.SetBool("land", true);
    }
    public void StopLand()
    {
        player.MyAnimator.SetBool("land", false);
    }
    public void Dig()
    {
        player.MyAnimator.SetTrigger("dig");
    }
    public void Protect()
    {
        player.MyAnimator.SetTrigger("protect");
    }
    public void Hurt()
    {
        player.MyAnimator.SetTrigger("hurt");
    }
    public void Die()
    {
        player.MyAnimator.SetTrigger("die");
    }

    public void Reset()
    {
        player.MyAnimator.SetBool("block", false);
    }
}
