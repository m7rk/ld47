using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractCharacter
{
    const float INVULN_TIME_MAX = 2f;

    public Vector2 lastVelocity;
    float invulnTime = 0;

    public HeartManager hm;

    private List<CharacterMove> allPlayerMoves = new List<CharacterMove>();

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        hm.setHearts(hp);
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

        CharacterMove CurrentMove = CharacterMoveToVector.vecToDir(moveVector);

        if (Input.GetKeyDown(KeyCode.J))
        {
            tryFire(projectileLaunchDirection,"PlayerProjectile");
            sprite.SetTrigger("shoot");
            allPlayerMoves.Add(CharacterMove.FIRE);
        } else
        {
            allPlayerMoves.Add(CurrentMove);
        }

        if (moveVector != Vector2.zero)
        {
            projectileLaunchDirection = moveVector;
        }

        applyForcesToRigidBody(moveVector, delta);

        lastVelocity = rb.velocity;
    }

    public void heal()
    {
        hp++;
        hm.setHearts(hp);
    }

    public override void hurt()
    {

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
        animateLizard(rb.velocity);
        invulnTime -= Time.deltaTime;
    }
}
