using UnityEngine;

public class Lightning : MonoBehaviour, ICollectable {

    public void Collect()
    {
        //TODO: increase attack speed
        Destroy(gameObject);
    }
}
