using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    private bool opened = false;
    // Update is called once per frame
    public void Open()
    {
        if (!opened)
        {
            GetComponent<Animator>().SetBool("boss_open", true);
            Destroy(transform.Find("DOORCLOSE").gameObject);
            opened = true;
        }

    }
}
