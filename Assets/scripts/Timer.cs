using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Variable to set time limit and keep track of remaining time
    private const float timeLimit = 15f;
    public float remainingTime;

    // Variable to take image in the UI
    // In order for this to work, set image type to "Filled", fill method to "Radial 360", and fill origin to "Top" and have clockwise checked
    public Image timerClock;

    // Text countdown
    public Text timeRemainDisplay;

    private Player p;

    public GameObject hurtOverlay;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = timeLimit;
        p = FindObjectOfType<Player>();
    }

    public void resetLimit()
    {
        remainingTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if(p.currentHP <= 0)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if(remainingTime + Time.deltaTime > 5f && remainingTime <= 5f)
        {
            FindObjectOfType<EnvSounds>().playClockWarnSound();

            //disgusting but w/e, it does force the reset.
            hurtOverlay.SetActive(false);
        }

        if (remainingTime < 0f)
        {
            remainingTime = timeLimit;
            slowPenalty();
        }

        timerClock.fillAmount = remainingTime / timeLimit; // produces the percent time remaining

        timeRemainDisplay.text = remainingTime.ToString("000"); // shows time to nearest whole number

        timeRemainDisplay.color = remainingTime > 5 ? Color.white : Color.red;
    }

    public void slowPenalty()
    {
        p.hurtIgnoreInvuln();

        FindObjectOfType<EnvSounds>().playTookClockDamageSound();

        hurtOverlay.SetActive(true);
    }

}
