using UnityEngine;

public class BattleArea : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<WaveSpawner>().enabled = true;
        }
    }
}
