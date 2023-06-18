using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
/*
public class GameManager : MonoBehaviour
{
    
    [SerializeField] Text highScore;

    private static int score;
    void Update()
    {
        score = TestLevel.score;
        HighScore();
        CheckHighScore();
        UpdateHighScoreText();
    }

    void HighScore()
    {
        
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.GetInt("HightScore");
    }

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void UpdateHighScoreText()
    {
        highScore.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
*/