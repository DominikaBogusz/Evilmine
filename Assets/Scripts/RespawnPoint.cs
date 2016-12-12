using UnityEngine;

public class RespawnPoint : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player.Instance.SetNewRespawnPoint(transform);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
