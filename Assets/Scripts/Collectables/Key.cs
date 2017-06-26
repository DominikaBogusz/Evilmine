using UnityEngine;

public class Key : MonoBehaviour, ICollectable {

    public void Collect()
    {
        //TODO: collect key
        Destroy(gameObject);
    }
}
