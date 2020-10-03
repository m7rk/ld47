using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for player, enemy, phantom.
public abstract class AbstractCharacter : MonoBehaviour
{
    // Enum for action that could be taken in any given frame.
    public enum CharacterMove
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT
    };

    public int hp = 3;

    public abstract void hurt();

    // Convert a vector to a charactermove.
    protected CharacterMove vecToDir(Vector2 d)
    {
        if (d == Vector2.zero)
        {
            return CharacterMove.NONE;
        }

        if (d == Vector2.left)
        {
            return CharacterMove.LEFT;
        }
        if (d == Vector2.right)
        {
            return CharacterMove.RIGHT;
        }
        if (d == Vector2.down)
        {
            return CharacterMove.DOWN;
        }
        if (d == Vector2.up)
        {
            return CharacterMove.UP;
        }

        if (d == new Vector2(1, 1))
        {
            return CharacterMove.UPRIGHT;
        }
        if (d == new Vector2(1, -1))
        {
            return CharacterMove.UPLEFT;
        }
        if (d == new Vector2(-1, -1))
        {
            return CharacterMove.DOWNLEFT;
        }
        if (d == new Vector2(-1, 1))
        {
            return CharacterMove.DOWNRIGHT;
        }

        Debug.Log("should have found vec");
        return CharacterMove.NONE;
    }

}
