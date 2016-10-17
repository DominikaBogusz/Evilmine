using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    private float speed = 5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        other.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Stay");
        if (other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            other.GetComponent<Animator>().SetBool("OnLadder", true);
        }
        else if (other.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            other.GetComponent<Animator>().SetBool("OnLadder", true);
        }
        else
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        other.GetComponent<Rigidbody2D>().gravityScale = 2;
        other.GetComponent<Animator>().SetBool("OnLadder", false);
    }
}
