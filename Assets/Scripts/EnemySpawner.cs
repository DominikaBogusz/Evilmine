using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public int minLevel;
    public int maxLevel;
    
    public GameObject[] prefabs;
}

public class EnemySpawner : MonoBehaviour {

    private static EnemySpawner instance;
    public static EnemySpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemySpawner>();
            }
            return instance;
        }
    }

    [SerializeField] private List<EnemyPrefab> enemyPrefabs;
    private List<GameObject> enemiesToSpawn;

    void Start()
    {
        enemiesToSpawn = new List<GameObject>();
    }

    public void DetermineEnemiesToSpawn(int expectedLevel)
    {
        foreach (EnemyPrefab enemyPrefab in enemyPrefabs)
        {
            if(expectedLevel >= enemyPrefab.minLevel && expectedLevel <= enemyPrefab.maxLevel)
            {
                foreach(GameObject enemy in enemyPrefab.prefabs)
                {
                    enemiesToSpawn.Add(enemy);
                }
            }
        }
    }

    public Enemy Spawn(Transform spawnPoint)
    {
        GameObject clone = Instantiate(enemiesToSpawn[Random.Range(0, enemiesToSpawn.Count)], spawnPoint.position, Quaternion.identity) as GameObject;

        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.LeftEdge = spawnPoint.GetChild(0);
        enemy.RightEdge = spawnPoint.GetChild(1);

        return enemy;
    }
}
