using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In order to use this script to fade in or out audio, use the following line in any other script:
// StartCoroutine(FadeAudioSource.StartFade(a,b,c));
// Where:
// a is an audiosource
// b is a float for length of fade in seconds
// c is a float for final volume between 0 (silent) and 1 (loud)

public static class FadeAudioSource
{

    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public static IEnumerator ChangeTrack(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}