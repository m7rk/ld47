using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static int level = 1;


    public static int transformToLayer(Vector3 p)
    {
        return 10000 + -((int)(100 * p.y));
    }
}
