using UnityEngine;

public class Axe : MonoBehaviour, ICollectable {

    [SerializeField] private float bonusValue;
    [SerializeField] private float bonusTime;

    public void Collect()
    {
        BonusManager.Instance.IncreaseDamage(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
