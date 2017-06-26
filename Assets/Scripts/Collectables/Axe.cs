using UnityEngine;

public class Axe : TimeBonus {

    public override void Collect()
    {
        BonusManager.Instance.IncreaseDamage((int)bonusValue, bonusTime);
        Destroy(gameObject);
    }
}
