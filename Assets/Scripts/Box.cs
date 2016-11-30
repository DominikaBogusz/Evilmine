using UnityEngine;
using System.Collections.Generic;

public class Box : MonoBehaviour {

    [SerializeField] private GameObject particlesPrefab;

    [SerializeField] private List<GameObject> dropsPrefabs;

	public void GenerateStuff()
    {
        GetComponentInParent<SpriteRenderer>().enabled = false;
        Instantiate(particlesPrefab, transform.position, Quaternion.identity);

        int random = Random.Range(0, dropsPrefabs.Count);
        Instantiate(dropsPrefabs[random], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
