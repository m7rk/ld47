using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Variable to set time limit and keep track of remaining time
    public float timeLimit = 10f;
    public float remainingTime;

    // Variable to take image in the UI
    // In order for this to work, set image type to "Filled", fill method to "Radial 360", and fill origin to "Top" and have clockwise checked
    public Image timerClock;

    // Text countdown
    public Text timeRemainDisplay;

    
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;

        if (remainingTime < 0f)
        {
            remainingTime = timeLimit;
            slowPenalty();
        }

        timerClock.fillAmount = remainingTime / timeLimit; // produces the percent time remaining

        timeRemainDisplay.text = remainingTime.ToString("000"); // shows time to nearest whole number
    }

    public void slowPenalty()
    {
        FindObjectOfType<Player>().hurtIgnoreInvuln();
    }

}
