using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    public Animator crossfadeAnimator;

    public void fadeToBlack()
    {
        crossfadeAnimator.SetTrigger("Start");
    }

}
