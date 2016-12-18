using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private List<GameObject> enemyPrefabs;

    public Enemy Spawn(Transform spawnPoint)
    {
        int number = DifficultyManager.Instance.ExpectedEnemyLevel / 5;
        GameObject clone = Instantiate(enemyPrefabs[number], spawnPoint.position, Quaternion.identity) as GameObject;

        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.LeftEdge = spawnPoint.GetChild(0);
        enemy.RightEdge = spawnPoint.GetChild(1);

        return enemy;
    }
}
