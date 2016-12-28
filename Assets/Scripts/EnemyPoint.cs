using UnityEngine;

public class EnemyPoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "SpawnDetector")
        {
            GetComponent<Collider2D>().enabled = false;
            EnemySpawner.Instance.Spawn(transform);
        } 
    }
}
