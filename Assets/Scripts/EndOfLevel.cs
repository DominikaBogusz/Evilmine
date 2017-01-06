using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour {

    [SerializeField] private List<GameObject> dontDestroyObjects; 

    void Awake()
    {
        foreach(GameObject go in dontDestroyObjects)
        {
            DontDestroyOnLoad(go);
        }
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIManager.Instance.ShowSummaryUI();
        }
    }
}
