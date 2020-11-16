using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheForgotten : Enemy
{
    public Animator animator;

    public float cooldownTime; //timer to reactivate teleports
    private const float minimumTeleport = 7f; //the fastest two teleports can be
    private const float maximumTeleport = 10f; //the slowest two teleports can be

    public float spawnTime = 1.5f;

    float originalX; //original position of boss, used to keep boss in bounds
    float originalY; //original position of boss, used to keep boss in bounds
    float originalZ; //original position of boss, used to keep boss in bounds

    int randX; //distance to move in X
    int randY; //distance to move in Y


    public RoomManager rtl;

    public void Awake()
    {
        currentHP = maxHealth();
        rtl = FindObjectOfType<RoomManager>();
    }

    void Start()
    {
        originalX = this.transform.position.x;
        originalY = this.transform.position.y;
        originalZ = this.transform.position.z;

        cooldownTime = 3;
    }

    public void fireOncePerSec()
    {
        if ((int)((1.5*cooldownTime) % 2) != (int)(((1.5 * cooldownTime + Time.deltaTime)) % 2))
        {
            for (int dx = -1; dx != 2; ++dx)
            {
                for (int dy = -1; dy != 2; ++dy)
                {
                    if (dx == 0 && dy == 0)
                    {
                        continue;
                    }
                    makeProjectile(new Vector2(dx, dy), "EnemyProjectile", Vector3.zero);
                    // lol
                    var ang = Mathf.Atan2(dy, dx) + (45 * Mathf.Deg2Rad);

                    makeProjectile(new Vector2(dx + Mathf.Cos(ang), dy + Mathf.Sin(ang)), "EnemyProjectile", Vector3.zero);

                    animator.SetBool("attack", true);
                }
            }
        }
    }


    public void Update()
    {
        if (!rtl.inRoom(this.transform.position) || currentHP <= 0)
        {
            return;

        }
        cooldownTime -= Time.deltaTime;
        spawnTime -= Time.deltaTime;

        if(spawnTime > 0f)
        {
            return;
        }

        if (cooldownTime > 4)
        {
            fireOncePerSec();
        }

        if (cooldownTime < 2f)
        {
            animator.SetBool("teleporting", true);
        }

        if (cooldownTime < 0)
        {
            randX = Random.Range(-3, 3);
            randY = Random.Range(-1, 1);

            if(currentHP < maxHealth())
            {
                currentHP++;
            }

            this.transform.position = new Vector3((originalX + randX), (originalY + randY), originalZ);

            cooldownTime = Random.Range(minimumTeleport, maximumTeleport);
            animator.SetBool("teleporting", false);
            spawnTime = 1.5f;

        }
    }

    public override void hurt()
    {
        playHurtSound();
        currentHP--;
        if (currentHP == 0)
        {
            animator.SetTrigger("die");
            GetComponent<BoxCollider2D>().enabled = false;
            nextLevel();
        }
    }

    public override int maxHealth()
    {
        return 20;
    }
}