using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    private Player player;

    private Animator animator;
    private enum animLayer { GROUND, AIR, LADDER };
    private animLayer currentAnimLayer;

    void Start ()
    {
        player = Player.Instance;
        animator = GetComponent<Animator>();
        currentAnimLayer = animLayer.GROUND;
    }
	
	void Update ()
    {
        HandleLayers();
	}

    public void Flip()
    {
        player.FacingRight = !player.FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
