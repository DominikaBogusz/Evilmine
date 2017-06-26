using UnityEngine;

public class Gem : MonoBehaviour, IUseable {

    private Player player;

    [SerializeField] private BoxCollider2D leftTrigger;
    [SerializeField] private BoxCollider2D rightTrigger;

    void Start()
    {
        player = Player.Instance;
    }

    public void Use()
    {
        if (leftTrigger.IsTouching(player.GetComponent<Collider2D>()) && !player.FacingRight)
        {
            player.AnimationManager.Flip();
        }
        else if (rightTrigger.IsTouching(player.GetComponent<Collider2D>()) && player.FacingRight)
        {
            player.AnimationManager.Flip();
        }
        player.Dig = true;
        player.UseManager.Useable = null;
        GetComponent<Collider2D>().enabled = false;
    }
}
