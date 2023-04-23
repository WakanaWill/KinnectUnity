using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeLeft = 60.0f; // The amount of time in seconds
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
        timerText.text = "Time left: " + Mathf.Round(timeLeft).ToString(); // Update the UI text

        if (timeLeft < 0)
        {
            testLevel.GameLost();
        }
    }
}