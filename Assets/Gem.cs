using UnityEngine;

public class Gem : MonoBehaviour, IUseable {

    [SerializeField]
    private BoxCollider2D leftTrigger;
    [SerializeField]
    private BoxCollider2D rightTrigger;

    public void Use()
    {
        Player.Instance.Dig = true;
    }
}
