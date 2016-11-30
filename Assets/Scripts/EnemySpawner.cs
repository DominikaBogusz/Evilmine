using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] private List<GameObject> enemyPrefabs;

    public void Spawn(Transform spawnPoint)
    {
        int randomEnemyNumber = Random.Range(0, enemyPrefabs.Count);
        GameObject clone = Instantiate(enemyPrefabs[randomEnemyNumber], spawnPoint.position, Quaternion.identity) as GameObject;
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.LeftEdge = spawnPoint.GetChild(0);
        enemy.RightEdge = spawnPoint.GetChild(1);
    }
}
