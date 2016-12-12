using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour {

	[SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;

    private bool activated;

    void Update()
    {
        if (activated)
        {
            if (!UIManager.Instance.ActiveAttributesUI)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                activated = false;
            }
        }
        else
        {
            if (UIManager.Instance.ActiveAttributesUI)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                activated = true;
            }
        }
    }

    public void UpdateHealth(float value)
    {
        healthValue.text = value.ToString();
    }

    public void UpdateDamage(float value)
    {
        damageValue.text = value.ToString();
    }

    public void UpdateAttackSpeed(float value)
    {
        attackSpeedValue.text = value.ToString();
    }
}
