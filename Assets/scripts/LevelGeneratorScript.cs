using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{
    #region variables responsible for square selection for boss, key, player

    int randNumbX;
    int randNumbY;

    int bossX;
    int bossY;

    int keyX;
    int keyY;

    int playerX;
    int playerY;

    bool bossroom = false;
    bool keyroom = false;
    bool playerroom = false;

    #endregion

    Vector3 positCheck;
    Vector3 mapScale = new Vector3(3, 3, 0);

    //This is needed to get the player and camera to start in the correct location
    public GameObject playerCharacter;

    public GameObject bossRoomPrefab;
    public GameObject keyRoomPrefab;
    public GameObject playerRoomPrefab;
    public GameObject genericRoomPrefab;

    public Vector3 bossPosition;
    public Vector3 keyPosition;
    public Vector3 playerPosition;

    public Quaternion prefabRotation = Quaternion.identity;


    // Start is called before the first frame update
    void Start()
    {
             
        // Start by determing location for three key elements on each map
        #region square selection for boss, key, player
        while (bossroom == false)
        {
            randNumbX = Random.Range(0, 3);
            randNumbY = Random.Range(0, 3);

            if (randNumbY != 1) // checking that coordinates for boss room are not in the middle row
            {
                bossX = randNumbX;
                bossY = randNumbY;
                bossPosition = new Vector3(bossX, bossY, 0f);
                bossroom = true;
            }
        }

        while (keyroom == false)
        {
            randNumbX = Random.Range(0, 3);
            randNumbY = Random.Range(0, 3);

            if (randNumbX != bossX && randNumbY != bossY) // checking that keyroom is not in same row or column as boss room
            {
                keyX = randNumbX;
                keyY = randNumbY;
                keyPosition = new Vector3(keyX, keyY, 0f);
                keyroom = true;
            }
        }

        while (playerroom == false)
        {
            randNumbX = Random.Range(0, 3);
            randNumbY = Random.Range(0, 3);

            if (randNumbX != bossX || randNumbY != bossY) // checking that player is not in boss room
            {
                if (randNumbX != keyX || randNumbY != keyY)
                {
                    playerX = randNumbX;
                    playerY = randNumbY;
                    playerPosition = new Vector3(playerX, playerY, 0f);
                    playerroom = true;
                }
            }
        }
        #endregion
        
        //Instantiate(bossDoor, bossPosition, prefabRotation);
        //Instantiate(leverRoom, keyPosition, prefabRotation);
        //Instantiate(playerStart, playerPosition, prefabRotation);

        //Set gameobject in each position
        #region square 0,0

        positCheck = new Vector3(0, 0, 0);
        
        if (bossPosition == positCheck)
        {
            placeBossRoom();
            
        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }

        #endregion

        #region square 0,1
        positCheck = new Vector3(0, 1, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 0,2
        positCheck = new Vector3(0, 2, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 1,0
        positCheck = new Vector3(1, 0, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 1,1
        positCheck = new Vector3(1, 1, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 1,2
        positCheck = new Vector3(1, 2, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 2,0
        positCheck = new Vector3(2, 0, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 2,1
        positCheck = new Vector3(2, 1, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

        #region square 2,2
        positCheck = new Vector3(2, 2, 0);

        if (bossPosition == positCheck)
        {
            placeBossRoom();

        }
        else if (keyPosition == positCheck)
        {
            placeKeyRoom();
        }
        else if (playerPosition == positCheck)
        {
            placePlayerRoom();
        }
        else
        {
            placeGenericRoom();
        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void placeBossRoom()
    {
        // place room with door to boss
        Instantiate(bossRoomPrefab, Vector3.Scale(positCheck, mapScale), Quaternion.identity);

        //place room with acutal boss
        if (positCheck[1] == 0)
        {
            // place boss room below
            Instantiate(bossRoomPrefab, Vector3.Scale(positCheck, mapScale) + (mapScale[1] * new Vector3(0, -1, 0)), Quaternion.identity);
        }
        else
        {
            // place boss room above
            Instantiate(bossRoomPrefab, Vector3.Scale(positCheck, mapScale) + (mapScale[1] * new Vector3(0, 1, 0)), Quaternion.identity);
        }
    }
    
    public void placeKeyRoom()
    {
        //place key room
        Instantiate(keyRoomPrefab, Vector3.Scale(positCheck, mapScale), Quaternion.identity);
    }

    public void placePlayerRoom()
    {
        //place player room
        Instantiate(playerRoomPrefab, Vector3.Scale(positCheck, mapScale), Quaternion.identity);

        //place camera in this room

        //place player in this room
        playerCharacter.transform.position = Vector3.Scale(positCheck, mapScale) + (.5f * mapScale);

        //teleport here if not first level?
    }

    public void placeGenericRoom()
    {
        //place generic room
        Instantiate(genericRoomPrefab, Vector3.Scale(positCheck, mapScale), Quaternion.identity);
    }

}
