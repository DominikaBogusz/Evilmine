using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    [SerializeField] public Attribute Level;
    [SerializeField] public Attribute Experience;
    [SerializeField] public Attribute ExperienceToNextLevel;
    [SerializeField] public Attribute Health;
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute ShieldProtection;

    public void Init()
    {
        Level.Set(Level.min);
        Experience.Set(Experience.min);
        ExperienceToNextLevel.Set(ExperienceToNextLevel.min);
        Health.Set(Health.max);
        Damage.Set(Damage.min);
        AttackSpeed.Set(AttackSpeed.min);
        ShieldProtection.Set(ShieldProtection.min);
    }

    public void GainExperience(int enemyLevel)
    {
        if (enemyLevel == Level)
        {
            Experience += 100;
            if (Experience >= ExperienceToNextLevel)
            {
                Level += 1;
                ExperienceToNextLevel += ExperienceToNextLevel.min * 2;
            }
        }
    }
}
