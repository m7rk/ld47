using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvSounds : MonoBehaviour
{
    public AudioClip clockWarning;
    public AudioClip tookClockDamage;
    public AudioClip openBossDoor;
    public AudioClip roomTransition;

    public void playClockWarnSound()
    {
        GetComponent<AudioSource>().PlayOneShot(clockWarning);
    }

    public void playTookClockDamageSound()
    {
        GetComponent<AudioSource>().PlayOneShot(tookClockDamage);

    }

    public void playOpenBossDoorSound()
    {
        GetComponent<AudioSource>().PlayOneShot(openBossDoor);

    }

    public void playRoomTransitionSound()
    {
        GetComponent<AudioSource>().PlayOneShot(roomTransition);

    }

}
