using UnityEngine;

public class Gem : MonoBehaviour, IUseable {

    [SerializeField]
    private BoxCollider2D leftTrigger;
    [SerializeField]
    private BoxCollider2D rightTrigger;

    public void Use()
    {
        if (leftTrigger.IsTouching(Player.Instance.GetComponent<Collider2D>()) && !Player.Instance.FacingRight)
        {
            Player.Instance.Flip();
        }
        else if (rightTrigger.IsTouching(Player.Instance.GetComponent<Collider2D>()) && Player.Instance.FacingRight)
        {
            Player.Instance.Flip();
        }
        Player.Instance.Dig = true;
        Player.Instance.Useable = null;
        GetComponent<Collider2D>().enabled = false;
    }
}
