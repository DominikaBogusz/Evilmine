using UnityEngine;

public class Lightning : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseAttackSpeed(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
