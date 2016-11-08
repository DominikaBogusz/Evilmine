using UnityEngine;

public class CameraBoundary : MonoBehaviour {

    [SerializeField] private CameraAdjust cam;

	void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision enter");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "LeftBar")
        {

        }
    }
}
