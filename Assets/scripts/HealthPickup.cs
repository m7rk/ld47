using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter2D(Collider2D character)
    {
             
        if (character.gameObject.CompareTag("Player"))
        {
            Player playerscript = character.GetComponent<Player>();

            if (playerscript.hp < 3)
            {
                player.heal();
                Destroy(gameObject);
            }
        }
        
    }

}
