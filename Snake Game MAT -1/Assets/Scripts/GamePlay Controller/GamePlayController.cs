using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class GamePlayController : MonoBehaviour
{
  public static GamePlayController instance ;

  public GameObject massBurner;

  public SnakeMovement snakeSegments;

  public GameObject snakeHead;

  public GameObject[] boundries;

  int score = 0 ;

  [SerializeField] TMP_Text scoreText;

  // Shield Variables  
  [SerializeField] Shield shieldScript;
  public GameObject shield;
  [SerializeField] GameObject shieldImage; 
  [SerializeField] Slider shieldSlider;
  public Animator shieldAnimator;

  
  // Timer Settings
  [SerializeField] float sliderBurnedRate;
  float Timer;
  public float maxTimer;


  // Speed Booster Variables  
  [SerializeField] SpeedBooster speedBoosterScript;
  public GameObject speedBooster;
  [SerializeField] Slider speedBoosterSlider;


  public GameObject gameOverPanel;
   
  void Awake()
  {
    Timer = maxTimer;
    makeInstance();
  }

    void makeInstance()
   {
       if(instance == null)
       {
          instance = this;
       }
   }


  void Update()
  {


    spawnMassBurner();

    spawnPowerUps();

    checkSpeedBooster();

    setShieldAnimationState();
  }

  void OnEnable()
  {
    SnakeHead.snakeHitSomething += snake_HitSomething;
  }


  void OnDisable()
  {
    SnakeHead.snakeHitSomething -= snake_HitSomething;
  }

   
   void snake_HitSomething(string item)
   {
       
    switch(item)
    {
      case "Apple":
     {
      for(int i=0 ; i<2; i++)
      {
        snakeSegments.snakeGrow();
      }
      score += 20;
     scoreText.text = "Score : " +score;
 
      break;
     }

      case "RedChilli":
     {
      for(int i=0 ; i<2; i++)
      {
        snakeSegments.snakeBurnMass();
      } 
      score -= 10; 
      break;
     }
      
      case "shield":
      {
        shieldScript.IsAbilityActivate = true;
        shieldSlider.value = 1f;
        break;
      }

      case "SpeedBooster":
      {
        speedBoosterScript.IsAbilityActivate = true;
        speedBoosterSlider.value = 1f;
        break;
      }

      case "Boundary":
     {
      changeSnakeDirection();
      // print("Hit Boundary");
      break;
     }

      case "SnakeBody":
     {
      checkForDeath();
      break;
     }
    }
  }

  void setShieldAnimationState()
  {
    if(shieldScript.IsAbilityActivate)
    {
      shieldImage.SetActive(true);
      shieldAnimator.ResetTrigger("Deactivate");
        
      shieldSlider.value -= sliderBurnedRate * Time.deltaTime;

      if(shieldSlider.value <= 0.4)
      {
        shieldAnimator.SetTrigger("Deactivate");
           
        if(shieldSlider.value <=0)
        {
          shieldScript.IsAbilityActivate = false;
        }
      }
    }
    if(!shieldScript.IsAbilityActivate)
    {
      StartCoroutine(deactivateShield());
    } 
  }

  IEnumerator deactivateShield()
  {      
    yield return new WaitForSeconds(0.5f);

    shieldImage.SetActive(false);
  }

  void checkSpeedBooster()
  {
    if(speedBoosterScript.IsAbilityActivate)
      {
        speedBoosterSlider.value -= sliderBurnedRate * Time.deltaTime;
        snakeSegments.speed = 0.03f;
    
        if(speedBoosterSlider.value <= 0)
        {
          speedBoosterScript.IsAbilityActivate = false;
        }
      }
      if(!speedBoosterScript.IsAbilityActivate)
      {
        snakeSegments.speed = 0.08f;
      }
  }


  void changeSnakeDirection()
  {
    if(snakeSegments.SnakeDirection == Vector2.left)
    {
      snakeHead.transform.position = new Vector3(
      boundries[3].transform.position.x+snakeSegments.SnakeDirection.x,
      Mathf.Round(snakeHead.transform.position.y)+snakeSegments.SnakeDirection.y,
      0f);
    //  print ( "Right Boundary");
    }
    if( snakeSegments.SnakeDirection == Vector2.right)
    {
       snakeHead.transform.position = new Vector3(
      boundries[2].transform.position.x+snakeSegments.SnakeDirection.x,
      Mathf.Round(snakeHead.transform.position.y)+snakeSegments.SnakeDirection.y,
      0f);
    //  print ( "Left Boundary");

    }
    if( snakeSegments.SnakeDirection == Vector2.up)
    {
      snakeHead.transform.position = new Vector3(
      Mathf.Round(snakeHead.transform.position.x)+snakeSegments.SnakeDirection.x,
      (boundries[1].transform.position.y+snakeSegments.SnakeDirection.y),
      0f);
    //  print ( "Bottom Boundary");

    }
    if( snakeSegments.SnakeDirection == Vector2.down)
    {
      snakeHead.transform.position = new Vector3(
      Mathf.Round(snakeHead.transform.position.x)+snakeSegments.SnakeDirection.x,
      (boundries[0].transform.position.y+snakeSegments.SnakeDirection.y),
     0f);

    //  print ( "Top Boundary");
    }
  }

  void checkForDeath()
  {
    if(!shieldScript.IsAbilityActivate)
      { 
       Time.timeScale = 0f;
       gameOverPanel.SetActive(true);
      }
  }

  void spawnMassBurner()
   {
     if ( snakeSegments.canSpawnMassBurner())
     {
        massBurner.SetActive(true);
     }
     else if (!snakeSegments.canSpawnMassBurner())
     {
       massBurner.SetActive(false);
     }
   }


   void spawnPowerUps()
   {
      Timer += Time.deltaTime;

      if(Timer > maxTimer)
      { 
       shieldScript.randomisePosition();
       shield.SetActive(true);

       speedBoosterScript.randomisePosition();
       speedBooster.SetActive(true);

       Timer = 0f;
      }
    }    

   
   public void Restart()
   {
     Time.timeScale = 1f;
     SceneManager.LoadScene(1);
   }

   public void Quit()
   {
     Time.timeScale = 1f;
     SceneManager.LoadScene(0);
   }
   


}


public enum Characters
{
  Snake = 1,
  Apple=2,
  Chilli=3,
  Shield=4,
  SpeedBooster=5,
  Boundary=6,
  SnakeBody=7,
}
