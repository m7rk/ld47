using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionListener : MonoBehaviour
{
    public Player p;
    int currRoomX = 0;
    int currRoomY = 0;

    float ROOM_SIZE_X = 14f;
    float ROOM_SIZE_Y = 9f;

    const float ROOM_OFFSET_X = 3f;
    const float ROOM_OFFSET_Y = 3f;

    // Update is called once per frame
    void Update()
    {
        int roomX = (int)((p.transform.position.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        int roomY = (int)((p.transform.position.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        
        if(roomX != currRoomX || roomY != currRoomY)
        {
            Camera.main.transform.position = new Vector3(4 + (roomX * ROOM_SIZE_X), 0.5f + (roomY * ROOM_SIZE_Y), -8.5f);
            currRoomX = roomX;
            currRoomY = roomY;
        }
    }
}
