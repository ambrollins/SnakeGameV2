using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shield : Food
{

[HideInInspector]public bool isAbilityActivated;
 
[SerializeField] private Slider shieldSlider;


public bool IsAbilityActivate 
{
  get { return isAbilityActivated; }
  
  set { isAbilityActivated = value;}
}


void Start()
{
  charType = Characters.Shield;
}

void OnTriggerEnter2D( Collider2D target)
{
  if ( target.tag == "Player")
  {
    gameObject.SetActive(false);
  }
}

void OnEnable()
{
  Invoke("DisableObject",3f);
  isAbilityActivated = false;
  shieldSlider.value = 0f;
}

void DisableObject()
{
  gameObject.SetActive(false);
}  

}
