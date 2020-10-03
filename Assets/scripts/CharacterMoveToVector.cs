using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We don't store big vectors. we store moves.
// this class converts betwee.
public class CharacterMoveToVector
{
    private static Map<AbstractCharacter.CharacterMove, Vector2> moveToVector = null;

    private static void initMap()
    {
        moveToVector = new Map<AbstractCharacter.CharacterMove, Vector2>();
        moveToVector.Add(AbstractCharacter.CharacterMove.NONE, Vector2.zero);

        moveToVector.Add(AbstractCharacter.CharacterMove.RIGHT, Vector2.right);
        moveToVector.Add(AbstractCharacter.CharacterMove.LEFT, Vector2.left);
        moveToVector.Add(AbstractCharacter.CharacterMove.UP, Vector2.up);
        moveToVector.Add(AbstractCharacter.CharacterMove.DOWN, Vector2.down);

        moveToVector.Add(AbstractCharacter.CharacterMove.DOWNLEFT, new Vector2(-1,-1));
        moveToVector.Add(AbstractCharacter.CharacterMove.DOWNRIGHT, new Vector2(-1, 1));
        moveToVector.Add(AbstractCharacter.CharacterMove.UPLEFT, new Vector2(1, -1));
        moveToVector.Add(AbstractCharacter.CharacterMove.UPRIGHT, new Vector2(1, 1));

        

    }
    // Convert a vector to a charactermove.
    public static AbstractCharacter.CharacterMove vecToDir(Vector2 d)
    { 
        if(moveToVector == null)
        {
            initMap();
        }
        return moveToVector.Reverse[d];
    }

    // Convert a charactermove to a vector.
    public static Vector2 DirToVec(AbstractCharacter.CharacterMove d)
    {
        if (moveToVector == null)
        {
            initMap();
        }
        return moveToVector.Forward[d];
    }
}

public class Map<T1, T2>
{
    private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
    private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

    public Map()
    {
        this.Forward = new Indexer<T1, T2>(_forward);
        this.Reverse = new Indexer<T2, T1>(_reverse);
    }

    public class Indexer<T3, T4>
    {
        private Dictionary<T3, T4> _dictionary;
        public Indexer(Dictionary<T3, T4> dictionary)
        {
            _dictionary = dictionary;
        }
        public T4 this[T3 index]
        {
            get { return _dictionary[index]; }
            set { _dictionary[index] = value; }
        }
    }

    public void Add(T1 t1, T2 t2)
    {
        _forward.Add(t1, t2);
        _reverse.Add(t2, t1);
    }

    public Indexer<T1, T2> Forward { get; private set; }
    public Indexer<T2, T1> Reverse { get; private set; }
}
