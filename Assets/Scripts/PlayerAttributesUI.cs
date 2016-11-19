using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesUI : AttributesUI {

    [SerializeField] private Text shieldProtectionValue;

    public void UpdateShieldProtection(int value)
    {
        shieldProtectionValue.text = value.ToString() + "%";
    }
}
