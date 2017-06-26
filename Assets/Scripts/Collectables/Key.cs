using UnityEngine;

public class Key : MonoBehaviour, ICollectable {

    private GameObject chest;

    public void SetChest(GameObject chest)
    {
        this.chest = chest;
    }

    public void Collect()
    {
        chest.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
