using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    public float timeRemaining;
    public bool timerIsRunning = false;
    public Text timeText;

    public void InitTimer()
    {
        timeRemaining = 60f;
        timerIsRunning = true;

    }

    public void UpdateTimer()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }


    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ClearText()
    {
        timeText.text = string.Empty;
    }

    public bool isTimeout()
    {
        return timeRemaining == 0 && timerIsRunning == false;
    }

    public string GetCurrTime()
    {
        return timeText.text;
    }
}
