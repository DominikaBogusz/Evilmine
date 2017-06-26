using UnityEngine;

public class EnemyPoint : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;

    void Start()
    {
        GameObject clone = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.LeftEdge = transform.GetChild(0);
        enemy.RightEdge = transform.GetChild(1);
    }
}
