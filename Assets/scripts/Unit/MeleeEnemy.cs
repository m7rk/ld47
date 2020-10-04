using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{


    private float scootTimeMax = 1f;

    private float scootTime = 0;

    private RoomManager rtl;

    public Animator sprite;

    Vector2 moveDir;

    const float speed = 2;

    public void Awake()
    {
        rtl = FindObjectOfType<RoomManager>();
    }

    public override void hurt()
    {
        Destroy(this.gameObject);
    }

    public override int maxHealth()
    {
        return 1;
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
        if(dir != Vector2.zero)
        {
            scootTime = scootTimeMax;
            moveDir = dir;
        }

        if(scootTime > 0)
        {
            GetComponent<Rigidbody2D>().velocity = speed * moveDir;

        } else
        {
            sprite.SetBool("walking", true);
            return;
        }

        if (moveDir.x < 0)
        {

            sprite.SetTrigger("lunge_left");
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        if (moveDir.x > 0)
        {
            sprite.SetTrigger("lunge_right");
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
