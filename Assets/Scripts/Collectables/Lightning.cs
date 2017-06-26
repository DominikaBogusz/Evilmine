using UnityEngine;

public class Lightning : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseAttackSpeed((int)bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
