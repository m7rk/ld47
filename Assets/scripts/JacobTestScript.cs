using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JacobTestScript : MonoBehaviour
{
    //public AudioSource soundfile;

    public AudioSource sound1;
    public AudioSource sound2;

    public AudioController ac;

    //public float rampspeed = 5f;
    
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Press space to fade out audio
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(FadeAudioSource.StartFade(soundfile, rampspeed, 0f));
        //}

        // Press return to fade in audio
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    StartCoroutine(FadeAudioSource.StartFade(soundfile, rampspeed, 1f));
        //}

        // End 1 music and start 2
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ac.changeTrack(sound1, sound2);
      
            //sound1.Play();
            //sound2.Stop();

        }

        // End 2 music and start 1
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //ac.changeTrack(sound2, sound1);


            //sound2.Play();
            //sound1.Stop();
        }

        
    }
}
