using UnityEngine;

public class BatPoint : MonoBehaviour {

    private Bat bat;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!bat && other.tag == "SpawnDetector")
        {
            bat = GetComponentInParent<BatSpawner>().Spawn(transform);
        }
    }
}
