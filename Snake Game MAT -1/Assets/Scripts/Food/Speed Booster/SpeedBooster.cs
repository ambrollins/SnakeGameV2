using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpeedBooster : Shield
{
 
[SerializeField]Slider speedBoosterSlider;


void Start()
{
    charType = Characters.SpeedBooster;
}



void OnEnable()
{
    Invoke("DisableObject",3f);
   isAbilityActivated = false;
    speedBoosterSlider.value = 0f;
}

void DisableObject()
{
    gameObject.SetActive(false);
}  

}
