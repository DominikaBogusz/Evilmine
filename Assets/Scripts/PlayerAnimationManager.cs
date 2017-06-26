using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour {

    private Player player;
    
    private enum AnimLayer { GROUND, AIR, LADDER };
    private AnimLayer currentAnimLayer;

    void Start ()
    {
        player = Player.Instance;
        currentAnimLayer = AnimLayer.GROUND;
    }
	
	void Update ()
    {
        HandleLayers();
	}

    public void HandleLayers()
    {
        switch (currentAnimLayer)
        {
            case AnimLayer.GROUND:
                if (!player.OnGround && !player.OnLadder)
                {
                    SwitchToLayer(AnimLayer.AIR);
                }
                if (player.OnLadder)
                {
                    SwitchToLayer(AnimLayer.LADDER);
                }
                break;
            case AnimLayer.AIR:
                if (player.OnGround && !player.OnLadder)
                {
                    player.MyAnimator.SetBool("jump", false);
                    SwitchToLayer(AnimLayer.GROUND);
                }
                if (player.OnLadder)
                {
                    SwitchToLayer(AnimLayer.LADDER);
                }
                break;
            case AnimLayer.LADDER:
                if (!player.OnLadder)
                {
                    SwitchToLayer(AnimLayer.GROUND);
                }
                break;
        }
    }

    private void SwitchToLayer(AnimLayer nextAnimLayer)
    {
        player.MyAnimator.SetLayerWeight((int)currentAnimLayer, 0);
        player.MyAnimator.SetLayerWeight((int)nextAnimLayer, 1);
        currentAnimLayer = nextAnimLayer;
    }

    public void SetMovementSpeed(float speed)
    {
        player.MyAnimator.SetFloat("speed", Mathf.Abs(speed));
    }
    public void SetAttackSpeed(float speed)
    {
        player.MyAnimator.SetFloat("attackSpeed", Mathf.Abs(speed));
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
    public void Damage()
    {
        player.MyAnimator.SetTrigger("damage");
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
