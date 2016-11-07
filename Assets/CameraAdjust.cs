using UnityEngine;

public class CameraAdjust : MonoBehaviour {

    [SerializeField] private Transform target;

    [SerializeField] private BoxCollider2D LevelBounds;

    private Vector3 min, max;

    void Start()
    {
        min = LevelBounds.bounds.min;
        max = LevelBounds.bounds.max;
    }


    void LateUpdate()
    {
        FollowTarget();
        AdjustToBounds();
    }

    private void FollowTarget()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void AdjustToBounds()
    {
        float cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);

        // lock the camera to the right or left bound if we are touching it
        float x = Mathf.Clamp(transform.position.x, min.x + cameraHalfWidth, max.x - cameraHalfWidth);

        // lock the camera to the top or bottom bound if we are touching it
        float y = Mathf.Clamp(transform.position.y, min.y + GetComponent<Camera>().orthographicSize, max.y - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
