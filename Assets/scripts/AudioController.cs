using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public float fadeTime = 5f;
    
    public void changeTrack(AudioSource audioOut, AudioSource audioIn)
    {
        //turn off current audio
        StartCoroutine(FadeAudioSource.StartFade(audioOut, fadeTime, 0f));

        //turn on new audio
        audioIn.volume = 0f;
        audioIn.Play();
        StartCoroutine(FadeAudioSource.StartFade(audioIn, fadeTime, 1f));
    }
}
