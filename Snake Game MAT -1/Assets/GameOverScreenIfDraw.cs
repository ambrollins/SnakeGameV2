using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverScreenIfDraw : MonoBehaviour
{
    public Text scoreText;
    public void SetUp(int score = 10)
    {
        gameObject.SetActive(true);
        scoreText.text = "score :" + score.ToString();
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
