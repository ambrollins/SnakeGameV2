using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public void Pause()
    {        
        Debug.Log("Game Paused");
    }
    public void Resume()
    {
        Debug.Log("Game Resumed");
    }
    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
