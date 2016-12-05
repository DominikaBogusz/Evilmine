using UnityEngine;

public class Shield : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseShieldProtection(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
