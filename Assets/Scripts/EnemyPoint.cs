using UnityEngine;

public class EnemyPoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SpawnDetector")
        {
            GetComponent<Collider2D>().enabled = false;
            Enemy spawnedEnemy = EnemySpawner.Instance.Spawn(transform);
            if (GetComponent<KeyKeeper>() != null)
            {
                GetComponent<KeyKeeper>().SetEnemy(spawnedEnemy);
            }
        } 
    }
}
