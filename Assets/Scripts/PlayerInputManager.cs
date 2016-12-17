using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    private Player player;

    void Start ()
    {
        player = Player.Instance;
    }
	
	void Update ()
    {
        if(!player.TakingDamage && !player.IsDead && !player.Digging)
        {
            HandleInput();

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            player.Move(horizontal, vertical);
        } 
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.Attacking)
        {
            player.AnimationManager.SetAttackSpeed(player.Attributes.AttackSpeed.Get() / 10f);
            player.AnimationManager.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !player.Jumping && !player.OnLadder)
        {
            player.AnimationManager.StartJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !player.Blocking)
        {
            player.AnimationManager.StartBlock();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            player.AnimationManager.StopBlock();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            player.UseManager.Use();
        }
    }
}
