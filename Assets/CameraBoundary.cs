using UnityEngine;

public class CameraBoundary : MonoBehaviour {

    [SerializeField] private CameraAdjust cam;

	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "RightBar")
        {
            cam.ActiveBoundaries.Add(GetComponent<BoxCollider2D>());
            Debug.Log("added");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "LeftBar")
        {
            cam.ActiveBoundaries.Remove(GetComponent<BoxCollider2D>());
            Debug.Log("removed");
        }
    }
}
