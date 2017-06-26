using UnityEngine;

public class Axe : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseDamage(bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
