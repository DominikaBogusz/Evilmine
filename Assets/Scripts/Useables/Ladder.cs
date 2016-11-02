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
            UseLadder(false, 1.5f);
        }
        else
        {
            UseLadder(true, 0f);
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, true); 
        }
    }

    private void UseLadder(bool onLadder, float gravity)
    {
        Player.Instance.OnLadder = onLadder;
        Player.Instance.MyRigidbody2D.gravityScale = gravity;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UseLadder(false, 1.5f);
            Physics2D.IgnoreCollision(Player.Instance.GetComponent<Collider2D>(), platformCollider, false);
        }
    }
}
