using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerActions : MonoBehaviour
{
    public enum Move
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }; 

    Move CurrentMove; 

    List<Move> allPlayerMoves;

    void Start()
    {
        CurrentMove = Move.NONE;
        allPlayerMoves = new List<Move>();
    }

    void Update()
    {
        CurrentMove = Move.NONE;

        if (Input.GetKeyDown(KeyCode.A))
        {
            CurrentMove = Move.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            CurrentMove = Move.RIGHT;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentMove = Move.UP;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            CurrentMove = Move.DOWN;
        }

        allPlayerMoves.Add(CurrentMove);
    }

    // playerDies()
    // {
    // export allPlayerMoves to the phantom
    // reset allPlayerMoves
    // }

 }
