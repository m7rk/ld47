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

    private float fallIntoPitTime = 0f;
    private Vector3 pitTarget;

    public GameObject pitCollider;

    

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
        currentHP = 3;
        hm.setHearts(3);
        am.setArrow(Vector2.right);

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

        if (Input.GetKeyDown(KeyCode.Space))
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
        CurrentMove.didFire = Vector2.zero;

        if (Input.GetKey(KeyCode.Space) && shootTimer <= 0)
        {
            startFire();
            CurrentMove.didFire = projectileLaunchDirection;
        }

        allPlayerMoves.Add(CurrentMove);



        if (moveVector != Vector2.zero)
        {
            if (shootTimer < shootTimerFrame)
            {
                projectileLaunchDirection = moveVector;
                am.setArrow(projectileLaunchDirection);
            }
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

        if (currentHP == 1)
        {
            currentHP--;
            hurtSound.Play();
            sprite.GetComponentsInChildren<SpriteRenderer>()[0].color = Color.red;
            sprite.GetComponentsInChildren<SpriteRenderer>()[1].color = Color.red;
            Invoke("ResetGame", 6f);
            cf.fadeToBlack();
            hm.setHearts(0);
            FindObjectOfType<AudioController>().changeTrack("ld47 gameover",false);
        }
        else if (currentHP > 1)
        {
            currentHP--;
            hurtSound.Play();
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
        if(invulnTime >= 0 )
        {
            return;
        }
        doHurt();

    }

    public void pitLogic()
    {
        this.transform.localScale = fallIntoPitTime * Vector2.one;
        fallIntoPitTime -= Time.deltaTime;
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, pitTarget, 1f - fallIntoPitTime);

        if (fallIntoPitTime <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (currentHP > 1)
            {
                FindObjectOfType<Player>().transform.position = FindObjectOfType<RoomManager>().playerRoomStartLoc;
                this.transform.localScale = Vector2.one;
            }

            hurtIgnoreInvuln();
            pitCollider.SetActive(true);
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

        if(currentHP <= 0)
        {
            return;
        }

        if(fallIntoPitTime > 0)
        {

            pitLogic();
            return;
        }

        playerInput(Time.deltaTime);
        animateLizard(rb.velocity.magnitude < ANIM_VEL_STOP_THRESH);

        invulnTime -= Time.deltaTime;
        footStepTime -= Time.deltaTime;

        checkForShoot("PlayerProjectile", SHOOT_OFFSET);

        sprite.GetComponentsInChildren<SpriteRenderer>()[0].color = (invulnTime > 0) ? Color.red : Color.white;
        sprite.GetComponentsInChildren<SpriteRenderer>()[1].color = (invulnTime > 0) ? Color.red : Color.white;
    }

    public override int maxHealth()
    {
        return 5;
    }

    void LateUpdate()
    {
        setRenderIndex();
    }

    public void FallIntoPit(Vector3 target)
    {
        pitCollider.SetActive(false);
        fallIntoPitTime = 1f;
        pitTarget = target;
    }
}
