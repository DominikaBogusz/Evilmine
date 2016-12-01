using UnityEngine;

public class Heart : MonoBehaviour, ICollectable {

    [SerializeField] private float bonusValue;

    public void Collect()
    {
        Player.Instance.Attributes.Health += bonusValue;
        Destroy(gameObject);
    }
}
