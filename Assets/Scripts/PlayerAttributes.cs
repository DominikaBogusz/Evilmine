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
    [SerializeField] [Range(5, 30)] private int initialDamage;
    [SerializeField] private Text damageText;

    public AttributeFloat AttackSpeed { get; set; }
    [SerializeField] [Range(0.5f, 2f)] private float initialAttackSpeed;
    [SerializeField] private Text attackSpeedText;

    public AttributeInt ShieldProtection { get; set; }
    [SerializeField] [Range(50, 100)] private int initialShieldProtection;
    [SerializeField] private Text shieldProtectionText;

    void Start()
    {
        Level = new AttributeInt("Level", 1, 100, levelText);
        Experience = new AttributeInt("Experience", 0, 99999, experienceText);
        ExperienceToNextLevel = new AttributeInt("ExperienceToNextLevel", 100, 9999, experienceToNextLevelText);

        Health = new AttributeInt("Health", 0, 100, healthText, healthIndicator);

        Damage = new AttributeInt("Damage", 5, 30, damageText);
        AttackSpeed = new AttributeFloat("AttackSpeed", 0.5f, 1.5f, attackSpeedText);
        ShieldProtection = new AttributeInt("ShieldProtection", 50, 100, shieldProtectionText);
    }

    public void Init()
    {
        Level.Set(Level.min);
        Experience.Set(Experience.min);
        ExperienceToNextLevel.Set(ExperienceToNextLevel.min);

        Health.Set(Health.max);

        Damage.Set(initialDamage);
        AttackSpeed.Set(initialAttackSpeed);
        ShieldProtection.Set(initialShieldProtection);
    }

    public void GainExperience(int enemyLevel)
    {
        if(enemyLevel == Level)
        {
            Experience += 100;
            if(Experience >= ExperienceToNextLevel)
            {
                Level += 1;
                ExperienceToNextLevel += ExperienceToNextLevel.min * 2;
            }
        }
    }
}
