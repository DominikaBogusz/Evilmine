using UnityEngine;

public class PlayerAnimationManager : AnimationManager {

    private Player player;
    
    private enum animLayer { GROUND, AIR, LADDER };
    private animLayer currentAnimLayer;

    public override void Start ()
    {
        base.Start();
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
                    animator.SetBool("jump", false);
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
        animator.SetLayerWeight((int)currentAnimLayer, 0);
        animator.SetLayerWeight((int)nextAnimLayer, 1);
        currentAnimLayer = nextAnimLayer;
    }

    public void SetMovementSpeed(float speed)
    {
        animator.SetFloat("speed", Mathf.Abs(speed));
    }
    public void SetClimbSpeed(float speed)
    {
        animator.SetFloat("climbSpeed", speed);
    }

    public void Attack()
    {
        animator.SetTrigger("attack");
    }
    public void StartBlock()
    {
        animator.SetBool("block", true);
    }
    public void StopBlock()
    {
        animator.SetBool("block", false);
    }
    public void StartJump()
    {
        animator.SetBool("jump", true);
    }
    public void StopJump()
    {
        animator.SetBool("jump", false);
    }
    public void StartLand()
    {
        animator.SetBool("land", true);
    }
    public void StopLand()
    {
        animator.SetBool("land", false);
    }
    public void Dig()
    {
        animator.SetTrigger("dig");
    }
}
