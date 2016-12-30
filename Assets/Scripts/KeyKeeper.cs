using UnityEngine;

public class KeyKeeper : MonoBehaviour {

	[SerializeField] private GameObject keyPrefab;
    [SerializeField] private GameObject referencedChest;

    private Enemy enemy;

    public void SetEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.DeadEvent += new DeadEventHandler(DropTheKey);
    }

    private void DropTheKey()
    {
        GameObject clone = Instantiate(keyPrefab, enemy.transform.position, Quaternion.identity) as GameObject;
        clone.GetComponent<Key>().SetChest(referencedChest);
    }
}
