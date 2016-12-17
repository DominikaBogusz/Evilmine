using UnityEngine;

public class EnemyAttributes : MonoBehaviour {

    [SerializeField] public Attribute Level;
    [SerializeField] public Attribute Health;
    [SerializeField] public Attribute Damage;
    [SerializeField] public Attribute AttackSpeed;
    [SerializeField] public Attribute AttackInterval;

    public void Init()
    {
        Level.Set(Level.min);
        Health.Set(100);
        Damage.Set(10);
        AttackSpeed.Set(10);
        AttackInterval.Set(80);
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
