using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;

    void setHearts(int i)
    {
        var v = Instantiate(heartPrefab);
        //v.transform
    }
}
