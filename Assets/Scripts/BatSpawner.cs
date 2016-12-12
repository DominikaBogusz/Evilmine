using UnityEngine;

public class BatSpawner : MonoBehaviour {

	[SerializeField] private GameObject batPrefab;

    public Bat Spawn(Transform spawnPoint)
    {
        float chance = DifficultyManager.Instance.PlayerLevel;
        float draw = Random.Range(0f, 100f);

        if (draw < chance)
        {
            GameObject clone = Instantiate(batPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            Bat bat = clone.GetComponent<Bat>();
            bat.LeftEdge = spawnPoint.GetChild(0);
            bat.RightEdge = spawnPoint.GetChild(1);

            return bat;
        }

        return null;
    }
}
