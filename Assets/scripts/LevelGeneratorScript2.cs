using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript2 : MonoBehaviour
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
    Vector3 mapScale = new Vector3(15, 10, 0);
    Vector3 shiftLevel = new Vector3(60, 0, 0);

    //This is needed to get the player and camera to start in the correct location
    //public GameObject playerCharacter;

    public GameObject[] bossDoorPrefab;
    public GameObject[] bossRoomPrefab;
    public GameObject[] keyRoomPrefab;
    public GameObject[] playerRoomPrefab;
    public GameObject[] genericRoomPrefab;

    GameObject roomInScene;
    int randPrefab;
    string[] doorNames;

    public Vector3 bossPosition;
    public Vector3 keyPosition;
    public Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {

        // Start by determing location for three key elements on each map
        #region square selection for boss, key, player
        while (bossroom == false)
        {
            randNumbX = Random.Range(0, 3);
            randNumbY = Random.Range(0, 3);

            if (randNumbY == 2) // checking that coordinates for boss room are in the top row, could eliminate bits of code but not worth it at this stage of game jam
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
        doorNames = new string[] { "doorU", "doorR" };
        roomSearch();
        #endregion

        #region square 0,1
        positCheck = new Vector3(0, 1, 0);
        doorNames = new string[] { "doorU", "doorR", "doorD" };
        roomSearch();
        #endregion

        #region square 0,2
        positCheck = new Vector3(0, 2, 0);
        doorNames = new string[] { "doorR", "doorD" };
        roomSearch();
        #endregion

        #region square 1,0
        positCheck = new Vector3(1, 0, 0);
        doorNames = new string[] { "doorU", "doorR", "doorL" };
        roomSearch();
        #endregion

        #region square 1,1
        positCheck = new Vector3(1, 1, 0);
        doorNames = new string[] { "doorU", "doorR", "doorD", "doorL" };
        roomSearch();
        #endregion

        #region square 1,2
        positCheck = new Vector3(1, 2, 0);
        doorNames = new string[] { "doorR", "doorD", "doorL" };
        roomSearch();
        #endregion

        #region square 2,0
        positCheck = new Vector3(2, 0, 0);
        doorNames = new string[] { "doorU", "doorL" };
        roomSearch();
        #endregion

        #region square 2,1
        positCheck = new Vector3(2, 1, 0);
        doorNames = new string[] { "doorU", "doorD", "doorL" };
        roomSearch();
        #endregion

        #region square 2,2
        positCheck = new Vector3(2, 2, 0);
        doorNames = new string[] { "doorD", "doorL" };
        roomSearch();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void placeBossRoom()
    {
        // place room with boss door
        randPrefab = Random.Range(0, bossDoorPrefab.Length);
        roomInScene = bossDoorPrefab[randPrefab];
        openDoors(roomInScene);
        roomInScene.transform.Find("doorU").gameObject.SetActive(false);
        Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + shiftLevel, Quaternion.identity);
        roomInScene.transform.Find("doorU").gameObject.SetActive(true);
        closeDoors(roomInScene);

        // place room with acutal boss
        randPrefab = Random.Range(0, bossRoomPrefab.Length);
        roomInScene = bossRoomPrefab[randPrefab];
        //openDoors(roomInScene);
        roomInScene.transform.Find("doorD").gameObject.SetActive(false);
        Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + (mapScale[1] * new Vector3(0, 1, 0)) + shiftLevel, Quaternion.identity);
        roomInScene.transform.Find("doorD").gameObject.SetActive(true);
        //closeDoors(roomInScene);
    }

    public void placeKeyRoom()
    {
        //place key room
        randPrefab = Random.Range(0, keyRoomPrefab.Length);
        roomInScene = keyRoomPrefab[randPrefab];
        openDoors(roomInScene);
        Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + shiftLevel, Quaternion.identity);
        closeDoors(roomInScene);
    }

    public void placePlayerRoom()
    {
        //place player room
        randPrefab = Random.Range(0, playerRoomPrefab.Length);
        roomInScene = playerRoomPrefab[randPrefab];
        openDoors(roomInScene);
        Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + shiftLevel, Quaternion.identity);
        closeDoors(roomInScene);

        //place camera in this room

        //place player in this room
        //playerCharacter.transform.position = Vector3.Scale(positCheck, mapScale) + (.5f * mapScale);

        //teleport here if not first level?
    }

    public void placeGenericRoom()
    {
        randPrefab = Random.Range(0, genericRoomPrefab.Length); // need to make sure this does all and not all - 1
        roomInScene = genericRoomPrefab[randPrefab];
        openDoors(roomInScene);
        Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + shiftLevel, Quaternion.identity);
        closeDoors(roomInScene);
    }

    public void roomSearch()
    {
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
    }

    public void openDoors(GameObject roomPrefab)
    {
        for (int i = 0; i < doorNames.Length; i++)
        {
            roomPrefab.transform.Find(doorNames[i]).gameObject.SetActive(false);
        }
    }

    public void closeDoors(GameObject roomPrefab)
    {
        for (int i = 0; i < doorNames.Length; i++)
        {
            roomPrefab.transform.Find(doorNames[i]).gameObject.SetActive(true);
        }
    }
}
