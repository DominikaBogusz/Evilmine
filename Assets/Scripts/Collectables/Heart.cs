using UnityEngine;

public class Heart : Bonus {

    public override void Collect()
    {
        Player.Instance.Attributes.Health.Set(Player.Instance.Attributes.Health + (int)bonusValue);
        Destroy(gameObject);
    }
}
