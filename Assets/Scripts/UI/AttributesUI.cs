﻿using UnityEngine;
using UnityEngine.UI;

public class AttributesUI : MonoBehaviour {

	[SerializeField] private Text healthValue;
    [SerializeField] private Text damageValue;
    [SerializeField] private Text attackSpeedValue;

    private bool activated;

    void Update()
    {
        if (activated != UIManager.Instance.ActiveAttributesUI)
        {
            ToggleUIActive();
        }
    }

    public void ToggleUIActive()
    {
        activated = !activated;
        transform.GetChild(0).gameObject.SetActive(activated);
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