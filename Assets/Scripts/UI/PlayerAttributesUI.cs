using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesUI : AttributesUI {

    [SerializeField] private Text shieldProtectionValue;

    public void UpdateShieldProtection(float value)
    {
        shieldProtectionValue.text = value.ToString() + "%";
    }

    [SerializeField] private Text levelValue;

    public void UpdateLevel(int value)
    {
        levelValue.text = value.ToString();
    }

    [SerializeField] private Text experienceValue;

    public void UpdateExperience(int value)
    {
        experienceValue.text = "( " + value.ToString();
    }

    [SerializeField] private Text nextLevelExperienceValue;

    public void UpdateNextLevelExperience(int value)
    {
        nextLevelExperienceValue.text = value.ToString() + " )";
    }
}
