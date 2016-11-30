using UnityEngine;

public class Heart : MonoBehaviour, ICollectable {

    public void Collect()
    {
        //TODO: increase health
        Destroy(gameObject);
    }
}
