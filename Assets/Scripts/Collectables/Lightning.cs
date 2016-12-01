using UnityEngine;

public class Lightning : MonoBehaviour, ICollectable {

    [SerializeField] private float bonusValue;
    [SerializeField] private float bonusTime;

    public void Collect()
    {
        BonusManager.Instance.IncreaseAttackSpeed(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
