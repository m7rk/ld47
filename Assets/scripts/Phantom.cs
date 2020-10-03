using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Input(AbstractCharacter.CharacterMove move)
    {
        Vector2 direction = CharacterMoveToVector.DirToVec(move);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
