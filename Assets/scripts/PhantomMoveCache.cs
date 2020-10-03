using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMoveCache : MonoBehaviour
{

    List<List<AbstractCharacter.CharacterMove>> phantomMoves = new List<List<AbstractCharacter.CharacterMove>>();

    int phantomCount = 0;

    void Start()
    {
    }

    // When the player dies, playerActions calls this (sending the allPlayerMoves list)
    public void addNewPhantom(List<AbstractCharacter.CharacterMove> moves)
    {
        phantomMoves.Add(moves);
        phantomCount += 1;
    }

    // total count of phantoms allowed, delete old phantoms if this is exceeded
    public void setMaxPhantoms(int max)
    {
        if (phantomCount > max)
        {
            // Remove the first list from phantomMoves
            phantomCount -= 1; //would need to be more elegant if you could die more than once at a time
        }
    }

    // get a move from an older player run
    // need to fix how this part is named/where it goes
    public AbstractCharacter.CharacterMove getPhantomMove(int phantom, int moveIndex)
    {
        return phantomMoves[phantom][moveIndex];
        //TODO : might run into problems if moveindex exceeds amount of available data
        //should write code to destroy phantom in that case
    }

}
