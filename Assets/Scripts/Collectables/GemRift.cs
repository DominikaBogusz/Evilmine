using UnityEngine;

public class GemRift : MonoBehaviour, ICollectable {

    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.Instance.GetComponent<Collider2D>(), true);
    }

    public void Collect()
    {
        ScoreManager.AddGems(name);
        Destroy(gameObject);
    }
}
