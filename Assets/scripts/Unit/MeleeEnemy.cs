﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{


    private float scootTimeMax = 3f;

    private float scootTime = 0;

    private RoomManager rtl;

    public Animator sprite;

    Vector2 moveDir;

    public bool facingLeft;

    const float speed = 3;

    public const float LUNGE_DIST = 3;

    public void Awake()
    {
        rtl = FindObjectOfType<RoomManager>();
        currentHP = maxHealth();
    }

    public override void hurt()
    {
        playHurtSound();
        currentHP--;
        if (currentHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public override int maxHealth()
    {
        return 3;
    }

    public Vector2 canSeePlayer()
    {
        // this copypasta is deliccious
        List<Vector2> dirs = new List<Vector2>();
        dirs.Add(Vector2.left);
        dirs.Add(Vector2.up);
        dirs.Add(Vector2.down);
        dirs.Add(Vector2.right);


        foreach (var d in dirs)
        {
            RaycastHit2D info = Physics2D.Raycast(transform.position, d, 100, LayerMask.GetMask("World", "Enemy", "Pits", "Player"));

            if (info.collider != null && info.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                return d;
            }
        }

        return Vector2.zero;
    }

    public void Update()
    {
        if (!rtl.inRoom(this.transform.position))
        {
            return;
        }

        scootTime -= Time.deltaTime;

        var dir = canSeePlayer();
        
        // Direct line of sight to player. Full speed ahead.
        if(dir != Vector2.zero)
        {
            scootTime = scootTimeMax;
            moveDir = dir;

            if (moveDir.x < 0)
            {
                facingLeft = true;
            }
            if (moveDir.x > 0)
            {
                facingLeft = false;
            }

            if((this.transform.position - FindObjectOfType<Player>().transform.position).magnitude < LUNGE_DIST)
            {
                sprite.SetBool("walking", false);
                sprite.SetTrigger(facingLeft ? "lunge_left" : "lunge_right");

                GetComponent<Rigidbody2D>().velocity = 3f * moveDir;
            } else
            {
                sprite.SetBool("walking", true);
                GetComponent<Rigidbody2D>().velocity = 2.5f * moveDir;
            }

        }
        else
        {
            // No line of sight to player. Cool off.
            if (scootTime > 0)
            {
                GetComponent<Rigidbody2D>().velocity = 1.5f *  moveDir;
                sprite.SetBool("walking", true);
            } else
            {
                //stop looking
                sprite.SetBool("walking", false);
            }
        }

        if (facingLeft)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}