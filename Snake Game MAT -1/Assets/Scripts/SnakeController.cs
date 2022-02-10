﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    public List<Transform> snakeBody;
    public Transform segmentPrefab;
    public static SnakeController snakecontroller;

    private void Awake()
    {
        Debug.Log("Snake controller awake");
        snakeBody = new List<Transform>();
        snakeBody.Add(this.transform);
       // segments.Remove(this.transform);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
            {
                direction = Vector2.up;
            }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
            {
                direction = Vector2.down;
            }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
                direction = Vector2.left;
            }
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
            {
                direction = Vector2.right;
            }
    }

    private void FixedUpdate()
    {
        for(int i = snakeBody.Count-1; i>0; i--)
        {
            snakeBody[i].position = snakeBody[i - 1].position;   
        }
        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x,Mathf.Round(this.transform.position.y) + direction.y,0f);
    }
    
    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = snakeBody[snakeBody.Count - 1].position;
        snakeBody.Add(segment);
    }
   // private void Shrink()
   // {
  //      Transform segment = Instantiate(this.segmentPrefab);
   //     segment.position = segments[segments.Count - 1].position;
   //     segments.Remove(segment);
   // }
    private void StartGame()
    {
        for (int i =1;i < snakeBody.Count; i++ )
        {
            Destroy(snakeBody[i].gameObject);
        }
        snakeBody.Clear();
        snakeBody.Add(this.transform);
        for (int i = 1; i < snakeBody.Count; i++)
        {
            snakeBody.Add(Instantiate(this.segmentPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    private void ScreenWrap(Collider2D collision)
    {
        if ( direction == Vector2.right )
        {
            this.transform.position = new Vector3((-1 * collision.transform.position.x) + 1, this.transform.position.y, 0f);
        }
        else if (direction == Vector2.left)
        {
            this.transform.position = new Vector3((-1 * collision.transform.position.x) -1, this.transform.position.y, 0f);
        }
        else if (direction == Vector2.up)
        {
            this.transform.position = new Vector3(this.transform.position.x ,(-1 * collision.transform.position.y)+1, 0f);
        }
        else if (direction == Vector2.down)
        {
            this.transform.position = new Vector3(this.transform.position.x, (-1 * collision.transform.position.y)-1, 0f);
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
        else if(collision.tag == "wall")
        {
            ScreenWrap(collision);
        }
        else if (collision.gameObject.GetComponent<SnakeController>())
        {
            FindObjectOfType<Game_Manager>().EndGame();
            StartGame();
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