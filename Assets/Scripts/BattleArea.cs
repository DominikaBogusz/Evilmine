using UnityEngine;

public class BattleArea : MonoBehaviour {

    [SerializeField] private Transform respawnPoint;

    public int EnemyCount { get; set;  }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<WaveSpawner>().enabled = true;
            Player.Instance.SetRespawnPoint(respawnPoint);
        }
    }

    public void IncreaseEnemyCount(Enemy enemy)
    {
        enemy.DeadEvent += new DeadEventHandler(DecreaseEnemyCount);
        EnemyCount++;
    }

    void DecreaseEnemyCount()
    {
        EnemyCount--;
    }

    public void StopBattle()
    {
        Destroy(gameObject);
        //transform.GetChild(0).gameObject.SetActive(false);
        //GetComponent<WaveSpawner>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
    }
}
