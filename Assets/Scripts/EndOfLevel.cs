using UnityEngine;

public class EndOfLevel : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIManager.Instance.ShowSummaryUI();
        }
    }
}
