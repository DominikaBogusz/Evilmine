using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    [SerializeField] public Attribute Level;
    [SerializeField] public Attribute Experience;
    [SerializeField] public Attribute ExperienceToNextLevel;
    [SerializeField] public Attribute Health;
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute ShieldProtection;

    public int LearningPoints { get; set; }

    public void Init()
    {
        Level.Set(1);
        Experience.Set(Experience.min);
        ExperienceToNextLevel.Set(ExperienceToNextLevel.min);
        Health.Set(Health.max);
        Damage.Set(Damage.min);
        AttackSpeed.Set(AttackSpeed.min);
        ShieldProtection.Set(ShieldProtection.min);
        LearningPoints = 0;
    }

    public void GainExperience(int enemyLevel)
    { 
        int experienceToGain = 10;

        int diff = enemyLevel - Level.Get();
        if (diff > 0)
        {
            experienceToGain = experienceToGain * (diff+1);
        }
        else if (diff < 0)
        {
            experienceToGain = experienceToGain / (-diff+1);
        }

        Experience += experienceToGain;
        CheckIfLevelUp();
    }

    private void CheckIfLevelUp()
    {
        if (Experience.Get() < ExperienceToNextLevel.Get())
        {
            return;
        }
        else
        {
            LearningPoints++;
            Level += 1;
            ExperienceToNextLevel += ExperienceToNextLevel.Get();
            DifficultyManager.Instance.ExpectedEnemyLevel++; 
            CheckIfLevelUp();
        }
    }
}
