using UnityEngine;

public class Box : MonoBehaviour {

    [SerializeField] private GameObject boxParticlesPrefab;

	public void GenerateStuff()
    {
        Instantiate(boxParticlesPrefab, transform.position, Quaternion.identity);

        GetComponentInParent<SpriteRenderer>().enabled = false;
        Destroy(gameObject);
    }
}
