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

    public float forceResetTimer = 15f;
    
    
    void Start()
    {
        playerRoomStartLoc = p.transform.position;
        currRoomX = (int)((playerRoomStartLoc.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        currRoomY = (int)((playerRoomStartLoc.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        Camera.main.GetComponent<CameraFollow>().target = new Vector3((currRoomX * ROOM_SIZE_X), (currRoomY * ROOM_SIZE_Y), -8.5f);
        Camera.main.transform.position = new Vector3((currRoomX * ROOM_SIZE_X), (currRoomY * ROOM_SIZE_Y), -8.5f);
    }

    void resetTimer()
    {
        timer.resetLimit();
    }

    void setDoor(Vector2 entry)
    {
        barrier.transform.position = roomCenter() + (entry * -new Vector2(ROOM_OFFSET_X+1.4f, ROOM_OFFSET_Y+1.4f));
    }

    public void startBossFight()
    {
        timer.gameObject.SetActive(false);
        switch(Utility.level)
        {
            case 1: FindObjectOfType<AudioController>().changeTrack("snd_floor1_boss",true); return;
            case 2: FindObjectOfType<AudioController>().changeTrack("snd_floor2_boss", true); return;
            case 3: FindObjectOfType<AudioController>().changeTrack("snd_floor3_boss", true); return;
        }
    }



    // Update is called once per frame
    void Update()
    {
        forceResetTimer -= Time.deltaTime;

        if(forceResetTimer <= 0f)
        {
            forceReset();
            forceResetTimer = 15f;
        }

        int roomX = (int)((p.transform.position.x + ROOM_OFFSET_X) / ROOM_SIZE_X);
        int roomY = (int)((p.transform.position.y + ROOM_OFFSET_Y) / ROOM_SIZE_Y);
        
        if(roomX != currRoomX || roomY != currRoomY)
        {
            forceResetTimer = 15f;
            FindObjectOfType<EnvSounds>().playRoomTransitionSound();
            FindObjectOfType<DungeonManager>().tryOpenBossDoor(roomX,roomY);





            resetTimer();
            Vector2 entry = new Vector2(roomX - currRoomX, roomY - currRoomY);

            if(roomY == 3)
            {
                startBossFight();
            }


            Camera.main.GetComponent<CameraFollow>().target = new Vector3((roomX * ROOM_SIZE_X), (roomY * ROOM_SIZE_Y), -8.5f);
            currRoomX = roomX;
            currRoomY = roomY;

            // player boost!
            FindObjectOfType<Player>().transform.position = FindObjectOfType<Player>().transform.position + 0.05f * (new Vector3(roomCenter().x, roomCenter().y, 0) - FindObjectOfType<Player>().transform.position);

            setDoor(entry);

            pm.Reset(p.flushMoves(), new Vector2(roomX * ROOM_SIZE_X, roomY * ROOM_SIZE_Y));

            playerRoomStartLoc = p.transform.position;
            // For the start loc, subtract a room size.
        }
    }

    public void forceReset()
    {
        pm.Reset(p.flushMoves(), new Vector2(currRoomX * ROOM_SIZE_X, currRoomY * ROOM_SIZE_Y));
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
