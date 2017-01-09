using System.Collections.Generic;
using UnityEngine;

public class LastBattle : MonoBehaviour {

    [SerializeField] private Transform respawnPoint;

    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> enemyPoints;

    [SerializeField] private GameObject kingPrefab;
    [SerializeField] private Transform kingPoint;

    [SerializeField] private GameObject chest;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.Instance.SetRespawnPoint(respawnPoint);
            StartBattle();
        }
    }

    private void StartBattle()
    {
        foreach(Transform point in enemyPoints)
        {
            EnemySpawnManager.Instance.Spawn(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], point);
        }

        Enemy king = EnemySpawnManager.Instance.Spawn(kingPrefab, kingPoint);
        kingPoint.GetComponent<KeyKeeper>().SetEnemy(king);
    }

    void Update()
    {
        if(chest == null)
        {
            LevelManager.Instance.LoadLevel(LevelManager.Instance.ActiveScene + 1);
        }
    }
}
