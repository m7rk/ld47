using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // put into new class

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frametw
    void Update()
    {
        
    }

    public void setup(Vector2 dir, string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        Destroy(this.gameObject);
    }
}
