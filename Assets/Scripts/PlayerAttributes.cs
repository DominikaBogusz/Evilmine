using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {

    public AttributeInt Level { get; set; }
    [SerializeField] private Text levelText;

    public AttributeInt Experience { get; set; }
    [SerializeField] private Text experienceText;

    public AttributeInt ExperienceToNextLevel;
    [SerializeField] private Text experienceToNextLevelText;

    public AttributeInt Health { get; set; }
    [SerializeField] private Text healthText;
    [SerializeField] private StatusIndicatorUI healthIndicator;

    public AttributeInt Damage { get; set; }
    [SerializeField] private Text damageText;

    public AttributeFloat AttackSpeed { get; set; }
    [SerializeField] private Text attackSpeedText;

    public AttributeInt ShieldProtection { get; set; }
    [SerializeField] private Text shieldProtectionText;

    void Start()
    {
        Level = new AttributeInt("Level", 1, 100, levelText);
        Experience = new AttributeInt("Experience", 0, 99999, experienceText);
        ExperienceToNextLevel = new AttributeInt("ExperienceToNextLevel", 0, 9999, experienceToNextLevelText);
        Health = new AttributeInt("Health", 0, 100, healthText, healthIndicator);
        Damage = new AttributeInt("Damage", 5, 20, damageText);
        AttackSpeed = new AttributeFloat("AttackSpeed", 0.8f, 1.4f, attackSpeedText);
        ShieldProtection = new AttributeInt("ShieldProtection", 60, 100, shieldProtectionText);
    }

    public void Init()
    {
        Level.Set(1);
        Experience.Set(0);
        ExperienceToNextLevel.Set(100);
        Health.Set(100);
        Damage.Set(10);
        AttackSpeed.Set(1f);
        ShieldProtection.Set(80);
    }
}
