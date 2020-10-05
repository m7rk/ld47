using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    private float fadeTime = 0.5f;
    public AudioSource playing;
    public AudioClip[] clips;

    string targTrack;
    bool loop;

    public void Start()
    {

        playing.clip = getClip(playMainTrack());
        playing.loop = true;
        playing.volume = 1f;
        playing.Play();
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

    public string playMainTrack()
    {
        switch(Utility.level)
        {
            case 1: return "snd_floor1";
            case 2: return "snd_floor2";
            case 3: return "snd_floor3";
        }
        return "";
    }

    public void playTargTrack()
    {
        //turn on new audio
        playing.volume = 1f;
        playing.clip = getClip(targTrack);
        playing.Play();
        playing.loop = loop;
    }
    public void changeTrack(string trackName, bool doloop)
    {
        targTrack = trackName;
        loop = doloop;
        //turn off current audio
        StartCoroutine(FadeAudioSource.StartFade(playing, fadeTime, 0f));
        // no race condition today
        Invoke("playTargTrack", fadeTime + 0.2f);
    }
}
