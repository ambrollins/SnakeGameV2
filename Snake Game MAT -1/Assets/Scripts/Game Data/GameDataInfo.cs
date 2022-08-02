using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataInfo 
{
   private static string MusicState = "Music_State";







  public void SeMusicState ( int state )
  {
    PlayerPrefs.SetInt(MusicState , state);
  }

  public int getMusicState()
  {
      return PlayerPrefs.GetInt(MusicState);
  }

    


}
