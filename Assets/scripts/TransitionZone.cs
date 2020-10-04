using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionZone : MonoBehaviour
{
    public string songToPlay;

    public void OnTriggerEnter2D()
    {
        FindObjectOfType<AudioController>().changeTrack("sfx_level complete1");
        Invoke("trackTransition", 8f);
        FindObjectOfType<PhantomManager>().clearPhantoms();
        FindObjectOfType<Player>().fullHeal();
        Progress.respawnPoint = this.transform.position;
    }

    public void trackTransition()
    {
        FindObjectOfType<AudioController>().changeTrack(songToPlay);
    }
}
