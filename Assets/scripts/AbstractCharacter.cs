﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for player, enemy, phantom.
public abstract class AbstractCharacter : MonoBehaviour
{
    // Default sounds for shooting weapon and getting hurt
    public AudioSource fireSound;
    public AudioSource hurtSound;

    // which way should projectiles be shot?
    protected Vector2 projectileLaunchDirection = Vector2.right;

    // Shooting timers for animations.
    public const float shootTimerMax = 2f;
    public float shootTimer = 0f;
    public float shootTimerFrame = 1.75f;


    public static float ANIM_VEL_STOP_THRESH = 0.1f;


    // Enum for action that could be taken in any given frame.
    public struct CharacterMove
    {
        public Vector2 location;
        public bool didFire;
    };

    
    public Animator sprite;

    public bool lookingLeft;

    public int hp = 3;

    public abstract void hurt();

    protected void animateLizard(bool stopped)
    {
        // idle
        if (stopped)
        {
            sprite.ResetTrigger("walk_right");
            sprite.ResetTrigger("walk_left");
            sprite.SetTrigger("idle");
            return;
        }

        // not idle.
        if (!lookingLeft)
        {
            sprite.ResetTrigger("walk_left");
            sprite.ResetTrigger("idle");
            sprite.SetTrigger("walk_right");
        }
        else
        {
            sprite.ResetTrigger("walk_right");
            sprite.ResetTrigger("idle");
            sprite.SetTrigger("walk_left");
        }
    }

    public void startFire()
    {
        sprite.SetTrigger("shoot");
        shootTimer = shootTimerMax;
        fireSound.Play();
    }

    public void makeProjectile(Vector2 dir, string layer)
    {
        var v = Instantiate(Resources.Load<GameObject>("Prefab/" + layer));
        v.GetComponent<Projectile>().setup(dir, layer);
        v.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
        v.layer = LayerMask.NameToLayer(layer);
        v.transform.position = this.transform.position;
    }

    public void checkForShoot(string projType)
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < shootTimerFrame && shootTimer + Time.deltaTime >= shootTimerFrame)
        {
            makeProjectile(projectileLaunchDirection, projType);
        }
    }


}


