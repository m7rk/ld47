using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundSource : MonoBehaviour
{
    public AudioClip[] hurtSounds;

    public void Play(int hp)
    {
        GetComponent<AudioSource>().PlayOneShot(hurtSounds[Mathf.Clamp(hp,0,hurtSounds.Length-1)]);
    }
}
