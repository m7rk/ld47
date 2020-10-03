using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : AbstractCharacter
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void hurt()
    {
        Destroy(this.gameObject);
    }

    public void Update()
    {

    }
}