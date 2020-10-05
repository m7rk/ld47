﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class JacobTestScript : MonoBehaviour
{
    public float cooldownTime; //timer to reactivate teleports
    public float minimumTeleport = 1f; //the fastest two teleports can be
    public float maximumTeleport = 3f; //the slowest two teleports can be

    float originalX; //original position of boss, used to keep boss in bounds
    float originalY; //original position of boss, used to keep boss in bounds
    float originalZ; //original position of boss, used to keep boss in bounds

    int randX; //distance to move in X
    int randY; //distance to move in Y

    // Start is called before the first frame update
    void Start()
    {
        originalX = this.transform.position.x;
        originalY = this.transform.position.y;
        originalZ = this.transform.position.z;

        cooldownTime = Random.Range(minimumTeleport, maximumTeleport);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTime -= Time.deltaTime; 

        if (cooldownTime < 0)
        {
            randX = Random.Range(0, 10);
            randY = Random.Range(0, 5);
            this.transform.position = new Vector3((originalX + randX), (originalY + randY), originalZ);
            cooldownTime = Random.Range(minimumTeleport, maximumTeleport);
            Debug.Log(cooldownTime);
        }

        // quick script to add stuff on button
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            //do something
        //}        
    }
}
