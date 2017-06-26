using UnityEngine;

public class Heart : Bonus {

    public override void Collect()
    {
        Player.Instance.Attributes.Health += bonusValue;
        Destroy(gameObject);
    }
}
