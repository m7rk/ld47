﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    const float velocity = 5;
    RoomManager rtl;

    // Start is called before the first frame update
    void Start()
    {
        rtl = FindObjectOfType<RoomManager>();
    }

    // Update is called once per frametw
    void Update()
    {
        // dont enter adjacent rooms!
        if(!rtl.inRoom(transform.position))
        {
            Destroy(this.gameObject);
        }
        var v = transform.Find("shadow");
        v.position = this.transform.position - new Vector3(0, 0.25f,0);
        
        int pos = Utility.transformToLayer(this.transform.position);
        var sr = this.GetComponent<SpriteRenderer>();
        if (sr)
        {
            sr.sortingOrder = pos;
        }

        GetComponent<ParticleSystemRenderer>().sortingOrder = pos;


    }

    public void setup(Vector2 dir, string layerName)
    {
        GetComponent<Rigidbody2D>().velocity = velocity * dir.normalized;
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        // see if a char was hit
        var character = c.gameObject.GetComponent<AbstractUnit>();
        if (character != null)
        {
            character.hurt();
        }
        Destroy(this.gameObject);
    }
}
