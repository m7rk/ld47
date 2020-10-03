using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public bool isScooting;

    public float scootTimeMax = 2f;
    public float shootTimeMax = 1f;

    public float stateTime = 0;

    public RoomManager rtl; 

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

    public Vector2 determineMoveDir()
    {
        // So, we need a pattern
        if(this.rtl.roomCenter().y > this.transform.position.y)
        {
            return Vector2.up;
        } else
        {
            return Vector2.down;
        }
    }

    public void createProjectile()
    {

    }

    public void Update()
    {
        stateTime -= Time.deltaTime;
        
        // I call it "scoot n shoot"
        if(isScooting)
        {
            if(stateTime <= 0)
            {
                isScooting = false;
                // trigger shoot anin
                makeProjectile(Vector2.left, "EnemyProjectile");
                stateTime = shootTimeMax; 
            }
        } else
        {
            if (stateTime <= 0)
            {
                isScooting = true;
                // trigger scoot
                GetComponent<Rigidbody2D>().AddForce(determineMoveDir(), ForceMode2D.Impulse);
                stateTime = scootTimeMax;
            }
        }
    }
}
