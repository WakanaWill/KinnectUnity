using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    float timeLeft; // The amount of time in seconds
    Text timerText;             // The UI Text object to display the timer
    TestLevel testLevel;
     
    private void Start()
    {
        timerText = GetComponent<Text>();
        testLevel = FindObjectOfType<TestLevel>();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime; // Subtract the elapsed time since last frame
        GUI(timeLeft);
        
        
        

        if (timeLeft < 0)
        {
            testLevel.GameLost();
            
        }
    }

    void GUI(float timeLeft)
    {
        float minutes = Mathf.Floor(timeLeft / 60);
        float seconds = Mathf.RoundToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetTimer(float time)
    {
        timeLeft = time;
    }
}