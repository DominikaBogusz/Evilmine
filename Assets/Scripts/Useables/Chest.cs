using UnityEngine;

public class Chest : MonoBehaviour, IUseable {

    private Player player;

    [SerializeField] private GameObject chestOpenPrefab;

    void Start()
    {
        player = Player.Instance;
    }

    public void Use()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;

        if (player.FacingRight)
        {
            player.MyRigidbody2D.position = new Vector2(player.transform.position.x - 0.8f, player.transform.position.y);
        }
        else
        {
            player.MyRigidbody2D.position = new Vector2(player.transform.position.x + 0.8f, player.transform.position.y);
        }

        player.UseManager.Useable = null;
        Destroy(gameObject);

        Instantiate(chestOpenPrefab, transform.position, Quaternion.identity);
    }
}
