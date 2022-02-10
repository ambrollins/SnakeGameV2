using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController2 : MonoBehaviour
{
    private Vector2 direction = Vector2.down;
    public List<Transform> segments;
    public Transform segmentPrefab;
    public static SnakeController snakecontroller;

    private void Awake()
    {
        Debug.Log("Snake controller awake");
        segments = new List<Transform>();
        segments.Add(this.transform);
        // segments.Remove(this.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }
    // private void Shrink()
    // {
    //      Transform segment = Instantiate(this.segmentPrefab);
    //     segment.position = segments[segments.Count - 1].position;
    //     segments.Remove(segment);
    // }
    private void StartGame()
    {

    }
    private void ScreenWrap(Collider2D collision)
    {
        if (direction == Vector2.right)
        {
            this.transform.position = new Vector3((-1 * collision.transform.position.x) + 1, this.transform.position.y, 0f);
        }
        else if (direction == Vector2.left)
        {
            this.transform.position = new Vector3((-1 * collision.transform.position.x) - 1, this.transform.position.y, 0f);
        }
        else if (direction == Vector2.up)
        {
            this.transform.position = new Vector3(this.transform.position.x, (-1 * collision.transform.position.y) + 1, 0f);
        }
        else if (direction == Vector2.down)
        {
            this.transform.position = new Vector3(this.transform.position.x, (-1 * collision.transform.position.y) - 1, 0f);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }
        // if (collision.tag == "Poison")
        //{
        // Shrink();
        //}
        else if (collision.tag == "wall")
        {
            ScreenWrap(collision);
        }
        else if (collision.gameObject.GetComponent<SnakeController>())
        {
            FindObjectOfType<Game_Manager>().EndGame();
        }
    }
    /*private void OnCollisionEnter2D(Collision2D gamecollision)
    {
        if (gamecollision.gameObject.GetComponent<SnakeController>())
        {
            FindObjectOfType<Game_Manager>().EndGame();
        }
    }*/
}
