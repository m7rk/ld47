using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPits : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var player = c.gameObject.GetComponent<Player>();

            player.hurtIgnoreInvuln();
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (player.currentHP > 0)
            {
                FindObjectOfType<Player>().transform.position = FindObjectOfType<RoomManager>().playerRoomStartLoc;
            }
        }
    }
}
