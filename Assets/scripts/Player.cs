using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float ACCEL_MULT = 100f;
    float MAX_VEL = 3f;
    Vector2 facing = Vector2.right;

    public enum PlayerMove
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT
    };


    List<PlayerMove> allPlayerMoves = new List<PlayerMove>();

    // Start is called before the first frame update
    void Start()
    {

    }

    PlayerMove vecToDir(Vector2 d)
    {
        if(d == Vector2.zero)
        {
            return PlayerMove.NONE;
        }

        if (d == Vector2.left)
        {
            return PlayerMove.LEFT;
        }
        if (d == Vector2.right)
        {
            return PlayerMove.RIGHT;
        }
        if (d == Vector2.down)
        {
            return PlayerMove.DOWN;
        }
        if (d == Vector2.up)
        {
            return PlayerMove.UP;
        }

        if (d == new Vector2(1,1))
        {
            return PlayerMove.UPRIGHT;
        }
        if (d == new Vector2(1, -1))
        {
            return PlayerMove.UPLEFT;
        }
        if (d == new Vector2(-1, -1))
        {
            return PlayerMove.DOWNLEFT;
        }
        if (d == new Vector2(-1, 1))
        {
            return PlayerMove.DOWNRIGHT;
        }

        Debug.Log("should have found vec");
        return PlayerMove.NONE;

    }
    void playerInput(float delta)
    {
        var rb = GetComponent<Rigidbody2D>();
        Vector2 vec = Vector2.zero;



        if (Input.GetKey(KeyCode.W))
        {
            vec += Vector2.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            vec += Vector2.down;
        }

        if (Input.GetKey(KeyCode.A))
        {
            vec += Vector2.left;

        } else if (Input.GetKey(KeyCode.D))
        {
            vec += Vector2.right;
        }

        PlayerMove CurrentMove = vecToDir(vec);
        allPlayerMoves.Add(CurrentMove);

        if (vec != Vector2.zero)
        {
            facing = vec;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            tryFire(facing);
        }

        rb.AddForce(vec * ACCEL_MULT * delta, ForceMode2D.Impulse);

        if(rb.velocity.magnitude > MAX_VEL)
        {
            rb.velocity = rb.velocity.normalized * MAX_VEL;
        }
    }

    public void tryFire(Vector2 dir)
    {
        var v = Instantiate(Resources.Load<GameObject>("fireball"));
        v.GetComponent<Projectile>().setup(dir,"PlayerProjectile");
        v.transform.position = this.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        playerInput(Time.deltaTime);
    }
}
