using UnityEngine;

public class Axe : MonoBehaviour, ICollectable {

    public void Collect()
    {
        //TODO: increase damage
        Destroy(gameObject);
    }
}
