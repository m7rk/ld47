using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : AbstractPlayerCharacter
{
    const float INVULN_TIME_MAX = 1f;
    public Rigidbody2D rb;
    const float ACCEL_MULT = 100f;
    const float MAX_VEL = 4f;

    public Vector2 lastVelocity;
    float invulnTime = 0;

    public HeartManager hm;
    public RoomManager rtl;
    public Crossfade cf;

    public AudioSource hurtSound;

    public AudioSource misc;
    public AudioClip[] footStepList;
    public AudioClip healSound;

    private List<CharacterMove> allPlayerMoves = new List<CharacterMove>();

    public const float footStepMax = 0.1f;
    public float footStepTime = 0f;

    public ArrowManager am;
    


    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        currentHP = maxHealth();
        hm.setHearts(currentHP);
        this.transform.position = Progress.respawnPoint;

    }

    void footStep()
    {
        if (footStepTime < 0)
        { 
            misc.PlayOneShot(footStepList[Random.Range(0, footStepList.Length)], 0.6f);
            footStepTime = footStepMax;
        }
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
        }

        allPlayerMoves.Add(CurrentMove);


        if (moveVector != Vector2.zero)
        {
            projectileLaunchDirection = moveVector;
            am.setArrow(projectileLaunchDirection);
            footStep();
        }

      

        applyForcesToRigidBody(moveVector, delta);

        lastVelocity = rb.velocity;
    }

    public void fullHeal()
    {
        currentHP = maxHealth();
        hm.setHearts(currentHP);
    }

    public bool heal()
    {
        if (currentHP < maxHealth())
        {
            misc.PlayOneShot(healSound);
            currentHP++;
            hm.setHearts(currentHP);
            return true;
        }
        return false;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void doHurt()
    {
        hurtSound.Play();
        currentHP--;
        if (currentHP == 0)
        {
            Invoke("ResetGame", 4.5f);
            cf.fadeToBlack();
            hm.setHearts(0);
            FindObjectOfType<AudioController>().changeTrack("ld47 gameover");
        }
        else if (currentHP > 0)
        {
            hm.setHearts(currentHP);
            this.invulnTime = INVULN_TIME_MAX;
        }
    }

    public void hurtIgnoreInvuln()
    {
        doHurt();
    }

    public override void hurt()
    {
        if(invulnTime >= 0)
        {
            return;
        }
        doHurt();

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

        if(currentHP <= 0)
        {
            return;
        }
        playerInput(Time.deltaTime);
        animateLizard(rb.velocity.magnitude < ANIM_VEL_STOP_THRESH);
        invulnTime -= Time.deltaTime;
        footStepTime -= Time.deltaTime;
        checkForShoot("PlayerProjectile");

        sprite.GetComponentsInChildren<SpriteRenderer>()[0].color = (invulnTime > 0) ? Color.red : Color.white;
        sprite.GetComponentsInChildren<SpriteRenderer>()[1].color = (invulnTime > 0) ? Color.red : Color.white;
    }

    public override int maxHealth()
    {
        return 3;
    }
}
