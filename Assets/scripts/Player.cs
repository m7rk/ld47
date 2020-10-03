using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractCharacter
{
    const float INVULN_TIME_MAX = 2f;
    public Rigidbody2D rb;
    const float ACCEL_MULT = 100f;
    const float MAX_VEL = 3f;
    const int MAX_HP = 3;

    public Vector2 lastVelocity;
    float invulnTime = 0;

    public HeartManager hm;
    public RoomTransitionListener rtl;
    public Crossfade cf;
   

    private List<CharacterMove> allPlayerMoves = new List<CharacterMove>();

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        hm.setHearts(hp);
    }



    protected void applyForcesToRigidBody(Vector2 moveVector, float delta)
    {
        rb.AddForce(moveVector * ACCEL_MULT * delta, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > MAX_VEL)
        {
            rb.velocity = rb.velocity.normalized * MAX_VEL;
        }

    }

    Vector2 getMoveVector()
    {
        Vector2 vec = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.J))
        {
            return vec;
        }

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
            lookingLeft = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            vec += Vector2.right;
            lookingLeft = false;

        }
        return vec;
    }

    void playerInput(float delta)
    {
        var moveVector = getMoveVector();

        CharacterMove CurrentMove;
        CurrentMove.location = rtl.removeOffsetFromRoom(this.transform.position);
        CurrentMove.didFire = false;

        if (Input.GetKey(KeyCode.J) && shootTimer <= 0)
        {
            startFire();
            CurrentMove.didFire = true;
            cf.fadeToBlack();
        }

        allPlayerMoves.Add(CurrentMove);


        if (moveVector != Vector2.zero)
        {
            projectileLaunchDirection = moveVector;
        }

        applyForcesToRigidBody(moveVector, delta);

        lastVelocity = rb.velocity;
    }

    public bool heal()
    {
        if (hp < MAX_HP)
        {
            hp++;
            hm.setHearts(hp);
            return true;
        }
        return false;
    }

    public override void hurt()
    {
        hurtSound.Play();
        hp--;
        if (hp <= 0)
        {
            this.transform.localPosition = new Vector3(0, 0, 0);
            hp = 3;
            hm.setHearts(hp);
        }
        else
        {
            hm.setHearts(hp);
            this.invulnTime = INVULN_TIME_MAX;
        }
    }

    public List<CharacterMove> flushMoves()
    {
        var ret = allPlayerMoves;
        allPlayerMoves = new List<CharacterMove>();
        return ret;
    }

    // Update is called once per frame
    void Update()
    {
        playerInput(Time.deltaTime);
        animateLizard(rb.velocity.magnitude < ANIM_VEL_STOP_THRESH);
        invulnTime -= Time.deltaTime;
        checkForShoot("PlayerProjectile");
    }
}
