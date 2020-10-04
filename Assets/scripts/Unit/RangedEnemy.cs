using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    private bool isScooting;

    private float scootTimeMax = 2f;

    private float shootTimeMax = 1.5f;
    private float shootTimeTrigger = 1f;
    private float projTile = 0.7f;

    private float stateTime = 0;

    private RoomManager rtl;

    public Animator sprite;

    Vector2 moveDir;

    const float speed = 1;

    public void Awake()
    {
        rtl = FindObjectOfType<RoomManager>();
    }

    public override void hurt()
    {
        playHurtSound();
        Destroy(this.gameObject);
    }

    public override int maxHealth()
    {
        return 1;
    }

    public Vector2 determineMoveDir()
    {
        // four raycasts.
        List<Vector2> dirs = new List<Vector2>();
        dirs.Add(Vector2.left);
        dirs.Add(Vector2.up);
        dirs.Add(Vector2.down);
        dirs.Add(Vector2.right);

        Vector2 bestDir = Vector2.zero;
        float bestDist = 0;

        foreach (var d in dirs)
        {
            RaycastHit2D info = Physics2D.Raycast(transform.position, d, 100, LayerMask.GetMask("World", "Enemy", "Pits"));
            // dont leave the room
            if (rtl.inRoom(info.point))
            {
                if (info.distance > bestDist)
                {
                    bestDist = info.distance;
                    bestDir = d;
                }
            }
        }

        return bestDir;
    }

    public Vector2 aimForPlayer()
    {
        var p = FindObjectOfType<Player>();
        var delta = p.transform.position - this.transform.position;

        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return new Vector2(Mathf.Sign(delta.x), 0);
        }
        else
        {
            return new Vector2(0, Mathf.Sign(delta.y));
        }
    }

    public void createProjectile()
    {

    }

    public void Update()
    {
        if (!rtl.inRoom(this.transform.position))
        {
            return;
        }

        stateTime -= Time.deltaTime;

        // I call it "scoot n shoot"
        if (isScooting)
        {
            GetComponent<Rigidbody2D>().velocity = speed * moveDir;
            sprite.SetBool("is_walking", true);
            if (stateTime <= 0)
            {
                isScooting = false;
                // trigger shoot anin

                stateTime = shootTimeMax;
            }
        }
        else
        {
            sprite.SetBool("is_walking", false);

            var v = aimForPlayer();
            if (v.x < 0)
            {
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            if (v.x > 0)
            {
                this.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (stateTime < shootTimeTrigger && stateTime + Time.deltaTime > shootTimeTrigger)
            {
                sprite.SetTrigger("shoot");
            }

            if (stateTime < projTile && stateTime + Time.deltaTime > projTile)
            {
                fireSound.Play();
                makeProjectile(v, "EnemyProjectile");
            }

            if (stateTime <= 0)
            {
                sprite.ResetTrigger("shoot");
                isScooting = true;
                // trigger scoot
                moveDir = determineMoveDir();
                if (moveDir.x < 0)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                if (moveDir.x > 0)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                stateTime = scootTimeMax;
            }
        }
    }
}
