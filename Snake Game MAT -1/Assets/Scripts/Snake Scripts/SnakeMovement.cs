using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
  private Vector2 snakeDirection;

  [HideInInspector] public float speed = 0.08f;

  [SerializeField] int initialSize = 4;

  private List<Transform> _segments;
 
  [SerializeField] Transform segmentPrefab;


  public Vector2 SnakeDirection
  {
    get{ return snakeDirection;}

    set{ snakeDirection = value; }
  }

 

  void Awake()
  {
    snakeDirection  = new Vector2(1f,0f);
    
    _segments = new List<Transform>();
    _segments.Add(this.transform);
  }


  void Start()
  {
    InitialSize();
  }



  void Update()
  {
    HandleInput();
  }

  
  void FixedUpdate()
  {
    snakeMovement(); 
  }

  protected virtual void HandleInput()
  {
    if(Input.GetKeyDown(KeyCode.UpArrow))
    {
      if((snakeDirection != Vector2.down))
      {     
        snakeDirection = Vector2.up;
        transform.eulerAngles = new Vector3(0,0,0);
      }
    }
   if(Input.GetKeyDown(KeyCode.DownArrow))
    {
      if((snakeDirection != Vector2.up ))
      {
        snakeDirection = Vector2.down;
        transform.eulerAngles = new Vector3(0,0,180f);
      }
    }
    if(Input.GetKeyDown(KeyCode.LeftArrow))
    {
      if((snakeDirection != Vector2.right))
      {  
        snakeDirection = Vector2.left;
        transform.eulerAngles = new Vector3(0,0,90f);
      }
    }
    if(Input.GetKeyDown(KeyCode.RightArrow))
    {
      if((snakeDirection != Vector2.left))
      {
        snakeDirection = Vector2.right;   
        transform.eulerAngles = new Vector3(0,0,270f);
      }
    }
  }

 void snakeMovement()
 {
    for (int i=_segments.Count - 1; i > 0 ; i--)
    {
      _segments[i].position = _segments[i-1].position;
    }

  // Set Speed for snake
  Time.fixedDeltaTime = speed;
  this.transform.position = new Vector3(
  Mathf.Round(this.transform.position.x)+snakeDirection.x,
  Mathf.Round(this.transform.position.y)+snakeDirection.y,
  0f);
 }


  public void snakeGrow()
  {
   Transform segment =  Instantiate(segmentPrefab).transform;
   segment.position = _segments[_segments.Count - 1].position;
   _segments.Add(segment);
  }


  public void snakeBurnMass()
  {
    Transform lastSegment = _segments[_segments.Count - 1];
    _segments.Remove(_segments[_segments.Count - 1]);
    Destroy(lastSegment.gameObject);
  }



 void InitialSize()
 {
   for ( int i = 1 ; i <= initialSize; i++)
    {
      snakeGrow();
    }

 }


  public bool canSpawnMassBurner()
 {
   if ( _segments.Count > (initialSize + 1))
   {
      return true;
   }
   return false; 
 }
}

