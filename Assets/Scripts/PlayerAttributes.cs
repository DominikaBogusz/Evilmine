using UnityEngine;

public class PlayerAttributes : Attributes {

    private PlayerAttributesUI playerAttributesUI;

    void Awake()
    {
        playerAttributesUI = attributesUI as PlayerAttributesUI;
    }

    [SerializeField] private float minShieldProtectionPercent;
    [SerializeField] private float maxShieldProtectionPercent;

    private float shieldProtectionPercent;
    public float ShieldProtectionPercent
    {
        get { return shieldProtectionPercent; }
        set
        {
            shieldProtectionPercent = Mathf.Clamp(value, minShieldProtectionPercent, maxShieldProtectionPercent);
            playerAttributesUI.UpdateShieldProtection(shieldProtectionPercent);
        }
    }

    private int level;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            playerAttributesUI.UpdateLevel(level);
            NextLevelExperience = level * nextLevelExperience;
        }
    }

    private int experience;
    public int Experience
    {
        get { return experience; }
        set
        {
            experience = value;
            playerAttributesUI.UpdateExperience(experience);
        }
    }

    private int nextLevelExperience;
    public int NextLevelExperience
    {
        get { return nextLevelExperience; }
        set
        {
            nextLevelExperience = value;
            playerAttributesUI.UpdateNextLevelExperience(nextLevelExperience);
        }
    }

    public override void Init()
    {
        base.Init();
        Damage = minDamage;
        AttackSpeed = minAttackSpeed;
        ShieldProtectionPercent = minShieldProtectionPercent;
        Level = 1;
        Experience = 0;
        NextLevelExperience = 100;
    }
}
