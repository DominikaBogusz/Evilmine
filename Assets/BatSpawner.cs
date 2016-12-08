﻿using UnityEngine;

public class BatSpawner : MonoBehaviour {

	[SerializeField] private GameObject batPrefab;

    public Bat bat { get; private set; }

    public Bat Spawn(Transform spawnPoint)
    {
        float chance = Player.Instance.Statistics.Level;
        float draw = Random.Range(0f, 100f);

        if (draw < chance)
        {
            GameObject clone = Instantiate(batPrefab, spawnPoint.position, Quaternion.identity) as GameObject;
            bat = clone.GetComponent<Bat>();
            bat.LeftEdge = spawnPoint.GetChild(0);
            bat.RightEdge = spawnPoint.GetChild(1);

            return bat;
        }

        return null;
    }
}
