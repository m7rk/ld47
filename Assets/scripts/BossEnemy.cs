using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public Animator animator;


    public void Awake()
    {
        currentHP = maxHealth();
    }

    public override void hurt()
    {
        playHurtSound();
        currentHP--;
        if (currentHP == 0)
        {
            animator.SetTrigger("die");
            GetComponent<BoxCollider2D>().enabled = false;
            nextLevel();
        }
    }

    public override int maxHealth()
    {
        return 3;
    }
}
