using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public string songToPlay;

    public void OnTriggerEnter2D()
    {
        FindObjectOfType<AudioController>().changeTrack(songToPlay);
        FindObjectOfType<PhantomManager>().clearPhantoms();
        FindObjectOfType<Player>().fullHeal();
        Progress.respawnPoint = this.transform.position;

    }
}
