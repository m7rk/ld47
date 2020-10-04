using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public List<GameObject> oldHearts = new List<GameObject>();

    public void setHearts(int i)
    {
        foreach(var v in oldHearts)
        {
            Destroy(v);
        }
        oldHearts.Clear();

        for (int j = 0; j != i; ++j)
        {
            var v = Instantiate(heartPrefab);
            v.transform.SetParent(this.transform);
            v.transform.localPosition = new Vector2(64 + 64 * j, 48);
            v.SetActive(true);
            oldHearts.Add(v);
        }
    }
}
