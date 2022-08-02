using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour
{
  
  public static event Action<string> snakeHitSomething = delegate { };

  [HideInInspector]public Characters charType;

  // [HideInInspector] int score;
 

  void Awake()
  {
    charType = Characters.Snake;
  }

  void OnTriggerEnter2D(Collider2D target)
  {
  
   switch(target.tag)
   {
    
    case  "Apple" : 
    {
      snakeHitSomething("Apple");      
      break;
    }
    case "RedChilli":
    {
      snakeHitSomething("RedChilli");
      break;
    }
    case "Shield" :
    {
      snakeHitSomething("shield");
      break;
    }
    case  "Speed":
    {
      snakeHitSomething("SpeedBooster");
      break;
    }
    case  "Boundary":
    {
      snakeHitSomething("Boundary");
      break;
    }
    case "SnakeBody":
    {
      snakeHitSomething("SnakeBody");
      break;
    }
     
   }


 }
}
