using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public float fadeTime = 5f;
    public AudioSource playing;
    public AudioSource next;
    public AudioClip[] clips;

    public void Start()
    {
        changeTrack("snd_floor1");
    }
    
    public AudioClip getClip(string name)
    {
        foreach(var v in clips)
        {
            if(v.name == name)
            {
                return v;
            }
        }
        return null;
    }

    public void changeTrack(string trackName)
    {
        //turn off current audio
        StartCoroutine(FadeAudioSource.StartFade(playing, fadeTime, 0f));

        //turn on new audio
        next.volume = 0f;
        next.clip = getClip(trackName);
        next.Play();
        StartCoroutine(FadeAudioSource.StartFade(next, fadeTime, 1f));
    }
}
