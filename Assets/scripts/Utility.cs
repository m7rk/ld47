using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Vector3 respawnPoint = new Vector3(-7, 0, 0);


    public static int transformToLayer(Vector3 p)
    {
        return 10000 + -((int)(100 * p.y));
    }
}
