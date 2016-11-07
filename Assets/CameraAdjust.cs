using UnityEngine;
using System.Collections.Generic;

public class CameraAdjust : MonoBehaviour {

    [SerializeField] private Transform target;

    //public List<BoxCollider2D> ActiveBoundaries { get; set; }
    [SerializeField] BoxCollider2D firstBoundaries;
    public List<BoxCollider2D> ActiveBoundaries;

    float left, right, up, down;

    [SerializeField] BoxCollider2D leftBar, rightBar, upBar, downBar;

    float cameraHalfWidth;

    void Start()
    {
        //ActiveBoundaries.Add(firstBoundaries);
        
        /*
        left = ActiveBoundaries[0].bounds.min.x;
        right = ActiveBoundaries[0].bounds.max.x;
        down = ActiveBoundaries[0].bounds.min.y;
        up = ActiveBoundaries[0].bounds.max.y;
        */

        cameraHalfWidth = GetComponent<Camera>().orthographicSize * ((float)Screen.width / Screen.height);
    }

    void Awake()
    {
        float height = 2 * GetComponent<Camera>().orthographicSize;
        float width = height * GetComponent<Camera>().aspect;

        leftBar.offset = new Vector2(-width / 2, 0f);
        rightBar.offset = new Vector2(width / 2, 0f);
        upBar.offset = new Vector2(0f, height / 2);
        downBar.offset = new Vector2(0f, -height / 2);
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
        if (ActiveBoundaries.Count == 1)
        {
            left = ActiveBoundaries[0].bounds.min.x;
            right = ActiveBoundaries[0].bounds.max.x;
            down = ActiveBoundaries[0].bounds.min.y;
            up = ActiveBoundaries[0].bounds.max.y;
        }
        else
        {
            foreach (BoxCollider2D box in ActiveBoundaries)
            {
                float aLeft = box.bounds.min.x;
                if (aLeft < left)
                {
                    left = aLeft;
                }
                float aRight = box.bounds.max.x;
                if (aRight > right)
                {
                    right = aRight;
                }
                float aDown = box.bounds.min.y;
                if (aDown < down)
                {
                    down = aDown;
                }
                float aUp = box.bounds.max.y;
                if (aUp > up)
                {
                    up = aUp;
                }
            }
        }

        // lock the camera to the right or left bound if we are touching it
        float x = Mathf.Clamp(transform.position.x, left + cameraHalfWidth, right - cameraHalfWidth);

        // lock the camera to the top or bottom bound if we are touching it
        float y = Mathf.Clamp(transform.position.y, down + GetComponent<Camera>().orthographicSize, up - GetComponent<Camera>().orthographicSize);

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
