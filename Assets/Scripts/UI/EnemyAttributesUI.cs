using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributesUI : AttributesUI {

    [SerializeField] private Text attackIntervalValue;

    public void UpdateAttackInterval(float value)
    {
        attackIntervalValue.text = value.ToString();
    }

    [SerializeField] private Text levelValue;

    public void UpdateLevel(int value)
    {
        levelValue.text = value.ToString();
    }
}
