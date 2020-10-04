using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject[] arrows;

    public void setArrow(Vector2 dir)
    {
        foreach(var a in arrows)
        {
            a.SetActive(false);
        }

        if(dir == new Vector2(1, 0))
        {
            arrows[0].SetActive(true);
        }
        if (dir == new Vector2(1, -1))
        {
            arrows[1].SetActive(true);
        }
        if (dir == new Vector2(0, -1))
        {
            arrows[2].SetActive(true);
        }
        if (dir == new Vector2(-1, -1))
        {
            arrows[3].SetActive(true);
        }
        if (dir == new Vector2(-1, 0))
        {
            arrows[4].SetActive(true);
        }
        if (dir == new Vector2(-1, 1))
        {
            arrows[5].SetActive(true);
        }
        if (dir == new Vector2(0, 1))
        {
            arrows[6].SetActive(true);
        }
        if (dir == new Vector2(1, 1))
        {
            arrows[7].SetActive(true);
        }
    }
}
