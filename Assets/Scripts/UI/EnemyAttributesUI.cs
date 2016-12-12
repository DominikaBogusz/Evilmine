using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributesUI : AttributesUI {

    [SerializeField] private Text attackIntervalValue;

    public void UpdateAttackInterval(float value)
    {
        attackIntervalValue.text = value.ToString();
    }
}
