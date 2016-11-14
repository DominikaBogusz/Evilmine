using UnityEngine;

public class EnemyPoints : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] enemyPoints;

    void Start ()
    {
        foreach (Transform t in enemyPoints)
        {
            GameObject clone = Instantiate(enemyPrefab, t.position, Quaternion.identity) as GameObject;

            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.LeftEdge = t.FindChild("LeftEdge");
            enemy.RightEdge = t.FindChild("RightEdge");
        } 
	}
}
