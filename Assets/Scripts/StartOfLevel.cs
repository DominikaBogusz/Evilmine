using UnityEngine;

public class StartOfLevel : MonoBehaviour {

    [SerializeField] private Transform startRespawnPoint;

    void Start()
    {
        Player.Instance.transform.position = transform.position;
        Player.Instance.SetRespawnPoint(startRespawnPoint);
    }
}
