using UnityEngine;

public class BatSpawner : MonoBehaviour {

	[SerializeField] private GameObject batPrefab;

    public void Spawn(Transform spawnPoint)
    {
        GameObject clone = Instantiate(batPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
        Bat bat = clone.GetComponent<Bat>();
        bat.LeftEdge = spawnPoint.GetChild(0);
        bat.RightEdge = spawnPoint.GetChild(1);
    }
}
