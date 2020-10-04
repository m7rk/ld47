using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : AbstractUnit
{

    // probably drop loot
    public AudioSource fireSound;
    private static List<AudioClip> hurtSounds;


    // Share hurt sounds. play based on HP
    public void playHurtSound()
    {
        FindObjectOfType<EnemySoundSource>().Play(currentHP);
    }

    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            c.gameObject.GetComponent<Player>().hurt();
        }
    }
}