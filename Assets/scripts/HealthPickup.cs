using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D character)
    {          
        Player playerscript = character.GetComponent<Player>();

        if (playerscript.heal())
        {           
            Destroy(gameObject);
        }
 
    }

}
