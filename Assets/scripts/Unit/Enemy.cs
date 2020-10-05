using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    //Call from a boss enemy. It is terrible style. But that is okay.
    public void nextLevel()
    {
        Utility.level++;

        FindObjectOfType<AudioController>().changeTrack("sfx_level complete1", false);
        Invoke("ResetGame", 7f);
        FindObjectOfType<Crossfade>().fadeToBlack();

        //this is also terrible but freezes input so okay whatever
        GetComponent<Player>().currentHP = 0;
    }

    public void resetGame()
    {
        SceneManager.LoadScene("Main");
    }
}