using UnityEngine;

public class Shield : MonoBehaviour, ICollectable {

    public void Collect()
    {
        //TODO: increase shield protection
        Destroy(gameObject);
    }
}
