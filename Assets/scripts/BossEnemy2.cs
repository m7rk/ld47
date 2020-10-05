using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy2 : Enemy
{
    public Animator animator;

    public float cooldownTime; //timer to reactivate teleports
    public float minimumTeleport = 1f; //the fastest two teleports can be
    public float maximumTeleport = 3f; //the slowest two teleports can be

    float originalX; //original position of boss, used to keep boss in bounds
    float originalY; //original position of boss, used to keep boss in bounds
    float originalZ; //original position of boss, used to keep boss in bounds

    int randX; //distance to move in X
    int randY; //distance to move in Y

    public void Awake()
    {
        currentHP = maxHealth();
    }

    void Start()
    {
        originalX = this.transform.position.x;
        originalY = this.transform.position.y;
        originalZ = this.transform.position.z;

        cooldownTime = Random.Range(minimumTeleport, maximumTeleport);
    }

    public void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime < 0)
        {
            randX = Random.Range(0, 10);
            randY = Random.Range(0, 5);
            this.transform.position = new Vector3((originalX + randX), (originalY + randY), originalZ);
            //an animation might look cool here, probably not enough time to implement I guess
            cooldownTime = Random.Range(minimumTeleport, maximumTeleport);
        }
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