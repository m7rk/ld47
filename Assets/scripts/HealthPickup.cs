using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private Vector3 startPos;

    void OnTriggerStay2D(Collider2D character)
    {          
        Player playerscript = character.GetComponent<Player>();

        if (playerscript.heal())
        {           
            Destroy(gameObject);
        }

    }

    public void Start()
    {
        startPos = this.transform.position;
    }

    public void Update()
    {
        // we have tween at home
        // tween at home
        this.transform.position = startPos + new Vector3(0, 0.1f * Mathf.Cos(2*Time.time),0);
    }

}
