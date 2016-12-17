using UnityEngine;

public class Heart : Bonus {

    public override void Collect()
    {
        Player.Instance.Attributes.Health.Set(Player.Instance.Attributes.Health.Get() + (int)bonusValue);
        Destroy(gameObject);
    }
}
