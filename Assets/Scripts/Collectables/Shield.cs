using UnityEngine;

public class Shield : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseShieldProtection((int)bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
