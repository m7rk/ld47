using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonPits : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log(c.name);
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<Player>().hurtIgnoreInvuln();
            FindObjectOfType<Player>().transform.position = FindObjectOfType<RoomManager>().playerRoomStartLoc;
        }
    }
}
