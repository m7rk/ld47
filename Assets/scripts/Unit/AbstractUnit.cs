using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    public int currentHP;
    public abstract void hurt();
    public abstract int maxHealth();
}
