using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : AbstractUnit
{

    // probably drop loot
    public AudioSource hurtSource;
    public AudioSource fireSound;
    public AudioClip[] hurtSounds;


    // Share hurt sounds. play based on HP
    public void playHurtSound()
    {
        fireSound.PlayOneShot(hurtSounds[currentHP]);
    }
    
}