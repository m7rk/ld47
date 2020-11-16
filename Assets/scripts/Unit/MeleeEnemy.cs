using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public bool isBoss;

    private float scootTimeMax = 3f;

    private float scootTime = 0;

    private RoomManager rtl;

    public Animator sprite;

    Vector2 moveDir;

    public bool facingLeft;

    const float speed = 3;

    public const float LUNGE_DIST = 4;

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
            sprite.SetTrigger("die");
            GetComponent<BoxCollider2D>().enabled = false;
            toCorpseLayer();

            if(isBoss)
            {
                nextLevel();
            }
        }
    }

    public override int maxHealth()
    {
        return isBoss ? 12 : 3;
    }

    public Vector2 canSeePlayer()
    {
        // this copypasta is deliccious
        List<Vector2> dirs = new List<Vector2>();
        dirs.Add(Vector2.left);
        dirs.Add(Vector2.up);
        dirs.Add(Vector2.down);
        dirs.Add(Vector2.right);

        List<Vector3> offsets = new List<Vector3>();
        offsets.Add(new Vector3(0.3f, 0.3f));
        offsets.Add(new Vector3(0f, 0f));
        offsets.Add(new Vector3(-0.3f, -0.3f));

        foreach (var d in dirs)
        {
            foreach (var o in offsets)
            {
                RaycastHit2D info = Physics2D.Raycast(transform.position + o, d, 100, LayerMask.GetMask("World", "Enemy", "Pits", "Player"));

                if (info.collider != null && info.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    return d;
                }
            }
        }

        return Vector2.zero;
    }

    public void Update()
    {
        if (!rtl.inRoom(this.transform.position) || currentHP <= 0)
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

                GetComponent<Rigidbody2D>().velocity = (isBoss ? 3f : 2f) * moveDir;
            } else
            {
                sprite.SetBool("walking", true);
                GetComponent<Rigidbody2D>().velocity = (isBoss ? 2.5f : 1.5f) * moveDir;
            }

        }
        else
        {
            // No line of sight to player. Cool off.
            if (scootTime > 0)
            {
                GetComponent<Rigidbody2D>().velocity = 1f *  moveDir;
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


    void LateUpdate()
    {
        if (currentHP <= 0)
        {
            return;
        }
        setRenderIndex();
    }
}
