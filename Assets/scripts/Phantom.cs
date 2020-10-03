using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : MonoBehaviour
{
    public enum Move
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    };

    List<List<Move>> phantomMoves;
    int phantomCount = 0;

    void Start()
    {
        phantomMoves = new List<List<Move>>();
    }

    // When the player dies, playerActions calls this (sending the allPlayerMoves list)
    public void addNewPhantom(List<Move> moves)
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
    public Phantom.Move getPhantomMove(int phantom, int moveIndex)
    {
        phantomAction = phantomMoves[phantom][moveIndex];
        //might run into problems if moveindex exceeds amount of available data
        //should write code to destroy phantom in that case
    }

}
