using UnityEngine;

public class Shield : MonoBehaviour, ICollectable {

    [SerializeField] private float bonusValue;
    [SerializeField] private float bonusTime;

    public void Collect()
    {
        BonusManager.Instance.IncreaseShieldProtection(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
