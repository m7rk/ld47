using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromoker : Enemy
{
    public Animator animator;

    private  Vector3 origin;

    public RoomManager rtl;

    float t1, t2;

    float t1delt = 0.4f;
    float t2delt = 5f;

    float patternReset = 15f;

    public void Awake()
    {
        currentHP = maxHealth();
        origin = this.transform.position;
        rtl = FindObjectOfType<RoomManager>();

    }

    public override void hurt()
    {
        playHurtSound();
        currentHP--;
        if (currentHP == 0)
        {
            animator.SetTrigger("die");
            GetComponent<Collider2D>().enabled = false;
            nextLevel();
        }


    }

    // fuckkkkkkkkkkkkkkkk
    public void fireOncePerSec()
    {
        if ((int)((patternReset) % 2) != (int)((((patternReset + Time.deltaTime))) % 2))
        {
            int dy = -1;
            for (int dx = -1; dx != 2; ++dx)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }
                makeProjectile(new Vector2(dx, dy), "EnemyProjectile", Vector3.zero);
                animator.SetBool("shoot", true);
            }
        }
    }

    public void Update()
    {
        if (!rtl.inRoom(this.transform.position) || currentHP <= 0)
        {
            return;
        }
        
        patternReset -= Time.deltaTime;

        if(patternReset < 0)
        {
            t1delt = Random.Range(0.2f, 0.4f);
            t2delt = Random.Range(3f, 5f);
            patternReset = 15f;
        }

        if (patternReset < 7)
        {
            fireOncePerSec();
        }

        t1 += t1delt * Time.deltaTime;
        t2 += t2delt * Time.deltaTime;



        this.transform.position = origin + new Vector3(3f * Mathf.Cos(t1), 0.25f * Mathf.Sin(t1 * 1.1f)) + new Vector3(Mathf.Sin(t2 ), 0.1f * Mathf.Cos(t2 * 1.1f));
    }


    public override int maxHealth()
    {
        return 10;
    }
}
