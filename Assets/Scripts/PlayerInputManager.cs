using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    private Player player;

    void Start ()
    {
        player = Player.Instance;
    }
	
	void Update ()
    {
        HandleInput();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        player.Move(horizontal, vertical);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.Attack)
        {
            player.AnimationManager.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !player.Jump && !player.OnLadder)
        {
            player.AnimationManager.StartJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !player.Block)
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
