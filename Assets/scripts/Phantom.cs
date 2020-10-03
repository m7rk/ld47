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
        Vector2 direction = CharacterMoveToVector.DirToVec(move);
        applyForcesToRigidBody(direction, Time.deltaTime);
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
