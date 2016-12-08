using UnityEngine;

public class BatPoint : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpawnDetector")
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponentInParent<BatSpawner>().Spawn(transform);
        }
    }
}
