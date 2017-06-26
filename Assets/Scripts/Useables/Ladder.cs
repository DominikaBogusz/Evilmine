using UnityEngine;

public class Ladder : MonoBehaviour, IUseable {

    private Player player;

    [SerializeField] private Collider2D platformCollider;

    void Start()
    {
        player = Player.Instance;
        Player.Instance.DeadEvent += new DeadEventHandler(GetOffLadder);
    }

    public void Use()
    {
        if (player.OnLadder)
        {
            GetOffLadder();
        }
        else
        {
            GetOnLadder();
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void GetOnLadder()
    {
        player.OnLadder = true;
        player.MyRigidbody2D.gravityScale = 0f;
        player.MyRigidbody2D.MovePosition(new Vector2(transform.position.x, player.MyRigidbody2D.position.y));
    }

    private void GetOffLadder()
    {
        player.OnLadder = false;
        player.MyRigidbody2D.gravityScale = 1.5f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "UseManager")
        {
            GetOffLadder();
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
