using UnityEngine;
using System.Collections.Generic;

public class Box : MonoBehaviour {

    [SerializeField] private GameObject particlesPrefab;

    [SerializeField] private List<GameObject> dropsPrefabs;

	public void GenerateStuff()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(particles, 1f);

        int random = Random.Range(0, dropsPrefabs.Count);
        Instantiate(dropsPrefabs[random], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
