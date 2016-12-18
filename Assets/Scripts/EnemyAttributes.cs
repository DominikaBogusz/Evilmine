using UnityEngine;

public class EnemyAttributes : MonoBehaviour {

    [SerializeField] public Attribute Level;
    [SerializeField] public Attribute Health;
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute AttackInterval;

    public void Init()
    {
        Level.Set(DifficultyManager.Instance.ExpectedEnemyLevel);
        Health.max = ((Health.min + Health.max) / Level.max) * Level.Get();
        Health.Set(Health.max);
        int damage = ((Damage.min + Damage.max) / Level.max) * Level.Get();
        Damage.Set(damage);
        int attackSpeed = ((AttackSpeed.min + AttackSpeed.max) / Level.max) * Level.Get();
        AttackSpeed.Set(attackSpeed);
        int attackInterval = ((AttackInterval.min + AttackInterval.max) / Level.max) * Level.Get();
        AttackInterval.Set(attackInterval);
    } 

    public void AccomodateToDifficultyLevel(string enemyName)
    {
    //    if (DifficultyManager.Instance.EnemyTypesDifficulty.ContainsKey(enemyName))
    //    {
    //        float level = DifficultyManager.Instance.EnemyTypesDifficulty[enemyName];

    //        //TODO jakieś gówno wszystko źle

    //        //Damage = (minDamage + maxDamage) / 2 * level;
    //        //AttackSpeed = (minAttackSpeed + maxAttackSpeed) / 2 * level;
    //        //AttackInterval = (minAttackInterval + maxAttackInterval) / 2 * (1-(level-1));
    //    }
    }
}
