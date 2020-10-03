using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : AbstractCharacter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Input(AbstractCharacter.CharacterMove move)
    {
        if (move == CharacterMove.FIRE)
        {
            tryFire(projectileLaunchDirection, "PhantomProjectile");
        }
        else
        {
            Vector2 direction = CharacterMoveToVector.DirToVec(move);
            applyForcesToRigidBody(direction, Time.deltaTime);

            if (direction.x > 0)
            {
                lookingLeft = false;
            }

            if (direction.x < 0)
            {
                lookingLeft = true;
            }

            if (direction != Vector2.zero)
            {
                projectileLaunchDirection = direction;
            }
        }

        animateLizard(rb.velocity);

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
