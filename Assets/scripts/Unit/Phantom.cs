using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : AbstractPlayerCharacter
{
    RoomManager rtl;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Input(AbstractPlayerCharacter.CharacterMove move)
    {
        if(rtl == null)
        {
            rtl = FindObjectOfType<RoomManager>();
        }

        Vector2 delta = new Vector2(transform.position.x, transform.position.y) - rtl.addOffsetToRoom(move.location);

        if (move.didFire)
        {
            startFire();
        }

        this.transform.position = rtl.addOffsetToRoom(move.location);
            
        if (delta.x < 0)
        {
            lookingLeft = false;
        }

        if (delta.x > 0)
        {
            lookingLeft = true;
        }

        if (delta != Vector2.zero)
        {
            projectileLaunchDirection = -delta;
        }

        animateLizard(delta.magnitude < 0.01);

    }
    // Update is called once per frame
    void Update()
    {

        checkForShoot("PhantomProjectile");
    }

    public override void hurt()
    {
        // lol
    }

    public override int maxHealth()
    {
        return 1;
    }
}
