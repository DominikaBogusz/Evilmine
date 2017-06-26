using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform target;

    private float height;
    private float width;

    private bool touchingLeft, touchingRight, touchingUp, touchingDown;
    private float left, right, up, down;

    private float x, y;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(20, 8, true);
        Physics2D.IgnoreLayerCollision(20, 9, true);
        Physics2D.IgnoreLayerCollision(20, 11, true);
        Physics2D.IgnoreLayerCollision(20, 13, true);
        Physics2D.IgnoreLayerCollision(20, 15, false);

        Physics2D.IgnoreLayerCollision(11, 13, true);
        Physics2D.IgnoreLayerCollision(11, 11, true);
    }

    void Awake()
    {
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }

    void LateUpdate()
    {
        if (touchingLeft)
        {
            x = Mathf.Clamp(transform.position.x, target.position.x, left + width / 2);
        }
        else if (touchingRight)
        {
            x = Mathf.Clamp(transform.position.x, right - width / 2, target.position.x);
        }
        else
        {
            x = target.position.x;
        }

        if (touchingDown)
        {
            y = Mathf.Clamp(transform.position.y, target.position.y, down + height / 2);
        }
        else if (touchingUp)
        {
            y = Mathf.Clamp(transform.position.y, up - height / 2, target.position.y);
        }
        else
        {
            y = target.position.y;
        }

        transform.position = new Vector3(x, y, transform.position.z);
        Camera.main.transform.position = new Vector3(x, y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        switch (other.name)
        {
            case "Left":
                //right part of boxCollider
                left = other.bounds.max.x;
                touchingLeft = true;
                break;
            case "Right":
                //left part of boxCollider
                right = other.bounds.min.x;
                touchingRight = true;
                break;
            case "Up":
                //bottom part of boxCollider
                up = other.bounds.min.y;
                touchingUp = true;
                break;
            case "Down":
                //upper part of boxCollider
                down = other.bounds.max.y;
                touchingDown = true;
                break;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.name)
        {
            case "Left":
                touchingLeft = false;
                break;
            case "Right":
                touchingRight = false;
                break;
            case "Up":
                touchingUp = false;
                break;
            case "Down":
                touchingDown = false;
                break;
        }
    }
}
