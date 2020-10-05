using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSounds : MonoBehaviour
{
    public AudioClip clockWarning;
    public AudioClip tookClockDamage;
    public AudioClip fallDown;
    public AudioClip roomTransition;

    public void playClockWarnSound()
    {
        GetComponent<AudioSource>().PlayOneShot(clockWarning);
    }

    public void playTookClockDamageSound()
    {
        GetComponent<AudioSource>().PlayOneShot(tookClockDamage);

    }

    public void playFallDown()
    {
        GetComponent<AudioSource>().PlayOneShot(fallDown, 0.8f);

    }

    public void playRoomTransitionSound()
    {
        GetComponent<AudioSource>().PlayOneShot(roomTransition);

    }

}
