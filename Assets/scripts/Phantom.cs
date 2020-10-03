using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : AbstractCharacter
{
    RoomTransitionListener rtl;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Input(AbstractCharacter.CharacterMove move)
    {
        if(rtl == null)
        {
            rtl = FindObjectOfType<RoomTransitionListener>();
        }

        Vector2 delta = new Vector2(transform.position.x, transform.position.y) - rtl.addOffsetToRoom(move.location);

        if (move.didFire)
        {
            tryFire(projectileLaunchDirection, "PhantomProjectile");
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
        
    }

    public override void hurt()
    {
        // lol
    }
}
