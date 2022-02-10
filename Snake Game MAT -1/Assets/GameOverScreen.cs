using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverScreen : MonoBehaviour
{
    public Text scoreText;
    public void SetUp(int score)
    {
        gameObject.SetActive (true);
        scoreText.text = "score :" + score.ToString();
    }
}
