using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public int minLevel;
    public int maxLevel;
    
    public GameObject[] prefabs;
}

public class EnemySpawnManager : MonoBehaviour {

    private static EnemySpawnManager instance;
    public static EnemySpawnManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemySpawnManager>();
            }
            return instance;
        }
    }

    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    private List<EnemyPrefab> enemiesToSpawn;

    void Start()
    {
        enemiesToSpawn = new List<EnemyPrefab>();

        LevelManager.Instance.GameplayPersistentObjects.Add(gameObject);
    }

    public void DetermineEnemiesToSpawn(int expectedLevel)
    {
        foreach (EnemyPrefab enemyPrefab in enemyPrefabs)
        {
            if(expectedLevel >= enemyPrefab.minLevel && expectedLevel <= enemyPrefab.maxLevel)
            {
                 enemiesToSpawn.Add(enemyPrefab);
            }
        }
    }

    public Enemy Spawn(Transform spawnPoint)
    {
        EnemyPrefab randomPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)];
        GameObject clone = Instantiate(randomPrefab.prefabs[Random.Range(0, randomPrefab.prefabs.Length)], spawnPoint.position, Quaternion.identity) as GameObject;

        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.GetComponent<EnemyAttributes>().Init(randomPrefab.minLevel, randomPrefab.maxLevel);
        enemy.LeftEdge = spawnPoint.GetChild(0);
        enemy.RightEdge = spawnPoint.GetChild(1);

        return enemy;
    }
}
