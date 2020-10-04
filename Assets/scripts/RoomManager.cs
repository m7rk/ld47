using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
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

    public GameObject barrier;
    
    
    void Start()
    {
        playerRoomStartLoc = p.transform.position;
        currRoomX = (int)((Progress.respawnPoint.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        currRoomY = (int)((Progress.respawnPoint.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        Camera.main.GetComponent<CameraFollow>().target = new Vector3((currRoomX * ROOM_SIZE_X), (currRoomY * ROOM_SIZE_Y), -8.5f);
    }

    void resetTimer()
    {
        timer.resetLimit();
    }

    void setDoor(Vector2 entry)
    {
        barrier.transform.position = roomCenter() + (entry * -new Vector2(ROOM_OFFSET_X+1.5f, ROOM_OFFSET_Y+1.5f));
    }



    // Update is called once per frame
    void Update()
    {
        int roomX = (int)((p.transform.position.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        int roomY = (int)((p.transform.position.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        
        if(roomX != currRoomX || roomY != currRoomY)
        {
            resetTimer();
            Vector2 entry = new Vector2(roomX - currRoomX, roomY - currRoomY);


            Camera.main.GetComponent<CameraFollow>().target = new Vector3((roomX * ROOM_SIZE_X), (roomY * ROOM_SIZE_Y), -8.5f);
            currRoomX = roomX;
            currRoomY = roomY;

            setDoor(entry);

            pm.Reset(p.flushMoves(), playerRoomStartLoc, new Vector2(roomX * ROOM_SIZE_X, roomY * ROOM_SIZE_Y));

            playerRoomStartLoc = p.transform.position;
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

    public bool inRoom(Vector3 v)
    {
        var centerX = currRoomX * ROOM_SIZE_X;
        var centerY = currRoomY * ROOM_SIZE_Y;

        return (Mathf.Abs(v.x - centerX) < (ROOM_SIZE_X / 2)) && (Mathf.Abs(v.y - centerY) < (ROOM_SIZE_Y / 2));
    }

    public Vector2 roomCenter()
    {
        var centerX = currRoomX * ROOM_SIZE_X;
        var centerY = currRoomY * ROOM_SIZE_Y;

        return new Vector2(centerX, centerY);
    }


}
