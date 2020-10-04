using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public string songToPlay;

    public void OnTriggerEnter2D()
    {
        Debug.Log("a");
        FindObjectOfType<AudioController>().changeTrack(songToPlay);
        FindObjectOfType<PhantomManager>().clearPhantoms();
        FindObjectOfType<Player>().fullHeal();
        
    }
}
