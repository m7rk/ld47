using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool wasTriggered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(wasTriggered)
        {
            return;
        }
        wasTriggered = true;
        GetComponent<Animator>().SetBool("lever_on", true);
        GetComponent<AudioSource>().Play();
    }
}
