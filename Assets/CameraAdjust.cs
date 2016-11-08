using UnityEngine;

public class CameraAdjust : MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraBody;

    public float left, right, up, down;

    void Start()
    {
        //Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), target.GetComponent<Collider2D>(), true);

        Physics2D.IgnoreLayerCollision(20, 8, true);
        Physics2D.IgnoreLayerCollision(20, 9, true);
        Physics2D.IgnoreLayerCollision(20, 15, false);

    }

    void Awake()
    {
        float height = 2 * GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;

        cameraBody.GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }


    void LateUpdate()
    {

        FollowTarget();
        transform.position = new Vector3(cameraBody.transform.position.x, cameraBody.transform.position.y, transform.position.z);

        //AdjustToBounds();
        //AdjustToBody();
    }

    private void FollowTarget()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void AdjustToBounds()
    {

        // lock the camera to the right or left bound if we are touching it
        //float x = Mathf.Clamp(transform.position.x, left + cameraHalfWidth, right - cameraHalfWidth);

        // lock the camera to the top or bottom bound if we are touching it
        //float y = Mathf.Clamp(transform.position.y, down + GetComponent<Camera>().orthographicSize, up - GetComponent<Camera>().orthographicSize);

        //transform.position = new Vector3(x, y, transform.position.z);
    }
}
