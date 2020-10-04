using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMoveCache
{
    // contains move data for one phantom
    private class PhantomMoveData
    {
        public List<AbstractPlayerCharacter.CharacterMove> moves;

        public PhantomMoveData(List<AbstractPlayerCharacter.CharacterMove> moves)
        {
            this.moves = moves;
        }
    }

    private List<PhantomMoveData> phantomMoves = new List<PhantomMoveData>();

    private int maxPhantoms = 0;

    public PhantomMoveCache()
    {
        maxPhantoms = 8;
    }

    // When the player dies or changes room, playerActions calls this (sending the allPlayerMoves list)
    public void addNewPhantom(List<AbstractPlayerCharacter.CharacterMove> moves)
    {
        phantomMoves.Insert(0,new PhantomMoveData(moves));
    }

    // total count of phantoms allowed, delete old phantoms if this is exceeded
    public void setMaxPhantoms(int max)
    {
        maxPhantoms = max;
        // delete phantom moves from end?
    }

    public int getMaxMovesForPhantom(int phantomIndex)
    {
        return phantomMoves[phantomIndex].moves.Count;
    }

    public int phantomCount()
    {
        return phantomMoves.Count;
    }

    // get a move from an older player run
    // need to fix how this part is named/where it goes
    public AbstractPlayerCharacter.CharacterMove getPhantomMove(int phantomIndex, int moveIndex)
    {
        return phantomMoves[phantomIndex].moves[moveIndex];
    }
}
