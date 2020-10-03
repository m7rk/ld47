using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    const float velocity = 5;

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
        GetComponent<Rigidbody2D>().velocity = velocity * dir.normalized;
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        // see if a char was hit
        var character = c.gameObject.GetComponent<AbstractCharacter>();
        if (character != null)
        {
            character.hurt();
        }
        Destroy(this.gameObject);
    }
}
