using UnityEngine;

public class Shoot : MonoBehaviour {

    [SerializeField] private GameObject missile;
    [SerializeField] private Transform missleStartPoint;

    public void ShootMissle()
    {
        Enemy enemy = GetComponent<Enemy>();
        GameObject clone;
        if (enemy.FacingRight)
        {
            clone = Instantiate(missile, missleStartPoint.position, Quaternion.identity) as GameObject;
        }
        else
        {
            clone = Instantiate(missile, missleStartPoint.position, Quaternion.Euler(new Vector3(0f, 0f, 180f))) as GameObject;
        }
        Missle missle = clone.GetComponent<Missle>();
        missle.Init(enemy);
    }


}
