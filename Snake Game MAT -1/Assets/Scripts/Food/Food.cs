using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
  [SerializeField] BoxCollider2D gridArea;

  public Characters charType; 


  public void randomisePosition()
 {
    Bounds bounds = gridArea.bounds;

    float x = Random.Range( bounds.min.x , bounds.max.x);
    float y = Random.Range( bounds.min.y, bounds.max.y);


    this.transform.position = new Vector2 ( x , y);

 }


}
