using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionListener : MonoBehaviour
{
    public Player p;
    public PhantomManager pm;
    public Vector2 playerRoomStartLoc;
    public Timer timer;

    int currRoomX = 0;
    int currRoomY = 0;

    float ROOM_SIZE_X = 15f;
    float ROOM_SIZE_Y = 10f;

    const float ROOM_OFFSET_X = 7.5f;
    const float ROOM_OFFSET_Y = 5f;


    
    void Start()
    {
        playerRoomStartLoc = p.transform.position;
    }

    void resetTimer()
    {
        timer.remainingTime = timer.timeLimit;
    }

    void nextArea()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int roomX = (int)((p.transform.position.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        int roomY = (int)((p.transform.position.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        
        if(roomX != currRoomX || roomY != currRoomY)
        {
            resetTimer();

            Camera.main.GetComponent<CameraFollow>().target = new Vector3((roomX * ROOM_SIZE_X), (roomY * ROOM_SIZE_Y), -8.5f);
            currRoomX = roomX;
            currRoomY = roomY;

            pm.Reset(p.flushMoves(), playerRoomStartLoc, new Vector2(roomX * ROOM_SIZE_X, roomY * ROOM_SIZE_Y));

            playerRoomStartLoc = removeOffsetFromRoom(p.transform.position);
            // For the start loc, subtract a room size.
        }
    }

    public Vector2 removeOffsetFromRoom(Vector2 v)
    {
        v.x -= currRoomX * ROOM_SIZE_X;
        v.y -= currRoomY * ROOM_SIZE_Y;
        return v;
    }

    public Vector2 addOffsetToRoom(Vector2 v)
    {
        v.x += currRoomX * ROOM_SIZE_X;
        v.y += currRoomY * ROOM_SIZE_Y;
        return v;
    }
}
