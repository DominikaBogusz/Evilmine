using UnityEngine;

public class Ladder : MonoBehaviour, IUseable {

    [SerializeField]
    private Collider2D platformCollider;

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}

    public void Use()
    {
        if (Player.Instance.OnLadder)
        {
            GetOffLadder();
        }
        else
        {
            GetOnLadder();
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, true);
        }
    }

    private void GetOnLadder()
    {
        Player.Instance.OnLadder = true;
        Player.Instance.MyRigidbody2D.gravityScale = 0f;
        Player.Instance.MyRigidbody2D.MovePosition(new Vector2(transform.position.x, Player.Instance.MyRigidbody2D.position.y));
    }

    private void GetOffLadder()
    {
        Player.Instance.OnLadder = false;
        Player.Instance.MyRigidbody2D.gravityScale = 1.5f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GetOffLadder();
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
