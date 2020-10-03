using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomMoveCache
{
    // contains move data for one phantom
    private class PhantomMoveData
    {
        public List<AbstractCharacter.CharacterMove> moves;
        public Vector3 startLoc;
        public PhantomMoveData(Vector3 startLoc, List<AbstractCharacter.CharacterMove> moves)
        {
            this.startLoc = startLoc;
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
    public void addNewPhantom(List<AbstractCharacter.CharacterMove> moves, Vector3 startLoc)
    {
        phantomMoves.Insert(0,new PhantomMoveData(startLoc,moves));
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
    public AbstractCharacter.CharacterMove getPhantomMove(int phantomIndex, int moveIndex)
    {
        return phantomMoves[phantomIndex].moves[moveIndex];
    }

    public Vector3 getStartPosition(int i)
    {
        return phantomMoves[i].startLoc;
    }

}
