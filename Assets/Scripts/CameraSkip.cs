using UnityEngine;

public class CameraSkip : MonoBehaviour {

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform[] cameraPositions;

    private float size = 8f;
    private float height;
    private float width;

    private float leftBoundary;
    private float rightBoundary;

    private float margin = 1f;

    void Start ()
    {
        Camera.main.orthographicSize = size;
        transform.position = cameraPositions[0].transform.position;

        height = 2 * size;
        width = height * Camera.main.aspect;
    }
	
	void Update ()
    {
        leftBoundary = transform.position.x - width / 2;
        rightBoundary = transform.position.x + width / 2;

        if (target.position.x > rightBoundary - margin)
        {
            transform.position = cameraPositions[1].transform.position;
        }
	}
}
