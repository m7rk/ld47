using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Crossfade : MonoBehaviour
{
    public Animator crossfadeAnimator;

    public void fadeToBlack()
    {
        crossfadeAnimator.SetTrigger("Start");

        // possible that it would be helpful to include some code to prevent movement or taking actions for a couple seconds
    }

}
