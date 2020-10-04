using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPits : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<Player>().hurtIgnoreInvuln();
            FindObjectOfType<Player>().transform.position = FindObjectOfType<RoomManager>().playerRoomStartLoc;
            FindObjectOfType<Player>().GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
