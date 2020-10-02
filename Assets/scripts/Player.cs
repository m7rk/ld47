using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float ACCEL_MULT = 20f;
    float MAX_VEL = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void playerInput(float delta)
    {

        Vector2 vec = Vector2.zero;

        if(Input.GetKey(KeyCode.W))
        {
            vec = Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            vec = Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vec = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vec = Vector2.right;
        }

        GetComponent<Rigidbody2D>().AddForce(vec * ACCEL_MULT * delta, ForceMode2D.Impulse);


    }

    // Update is called once per frame
    void Update()
    {
        playerInput(Time.deltaTime);
    }
}
