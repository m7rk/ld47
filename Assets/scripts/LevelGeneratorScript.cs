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

    private Vector3 positCheck;
    private Vector3 mapScale = new Vector3(15f, 10f, 0);

    //This is needed to get the player and camera to start in the correct location
    private GameObject playerCharacter;

    private GameObject[] bossDoorPrefab;
    private GameObject[] bossRoomPrefab;
    private GameObject[] keyRoomPrefab;
    private GameObject[] playerRoomPrefab;
    private GameObject[] genericRoomPrefab;

    GameObject roomInScene;
    int randPrefab;
    string[] doorNames;

    private Vector3 bossPosition;
    private Vector3 keyPosition;
    private Vector3 playerPosition;

    public Player player;

    private void preloadPrefabsForProgress()
    {
        bossDoorPrefab = Resources.LoadAll<GameObject>("Rooms/" + Utility.level + "/Boss Door");
        bossRoomPrefab = Resources.LoadAll<GameObject>("Rooms/" + Utility.level + "/Boss Room");
        keyRoomPrefab = Resources.LoadAll<GameObject>("Rooms/" + Utility.level + "/Key Room");
        playerRoomPrefab = Resources.LoadAll<GameObject>("Rooms/" + Utility.level + "/Player Room");
        genericRoomPrefab = Resources.LoadAll<GameObject>("Rooms/" + Utility.level + "/Other Room");
    }
    // Start is called before the first frame update
    void Awake()
    {
        preloadPrefabsForProgress();
             
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
        doorNames  = new string[] { "doorU", "doorR"};
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


    public void placeBossRoom()
    {
        // place room with boss door
        randPrefab = Random.Range(0, bossDoorPrefab.Length);
        roomInScene = bossDoorPrefab[randPrefab];
        openDoors(roomInScene);
        roomInScene.transform.Find("doorU").gameObject.SetActive(false);
        parent(Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale), Quaternion.identity));
        roomInScene.transform.Find("doorU").gameObject.SetActive(true);
        closeDoors(roomInScene);

        // place room with acutal boss
        randPrefab = Random.Range(0, bossRoomPrefab.Length); 
        roomInScene = bossRoomPrefab[randPrefab];
        roomInScene.transform.Find("doorD").gameObject.SetActive(false);
        parent(Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale) + (mapScale[1] * new Vector3(0, 1, 0)), Quaternion.identity));
        roomInScene.transform.Find("doorD").gameObject.SetActive(true);
    }
    
    public void placeKeyRoom()
    {
        //place key room
        randPrefab = Random.Range(0, keyRoomPrefab.Length); 
        roomInScene = keyRoomPrefab[randPrefab];
        openDoors(roomInScene);
        parent(Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale), Quaternion.identity));
        closeDoors(roomInScene);
    }

    public void placePlayerRoom()
    {
        //place player room
        randPrefab = Random.Range(0, playerRoomPrefab.Length); 
        roomInScene = playerRoomPrefab[randPrefab];
        openDoors(roomInScene);
        parent(Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale), Quaternion.identity));
        closeDoors(roomInScene);

        // This is where i will spawn the player. I just expose it and let the game manager handle it.
        player.transform.position = this.transform.position + Vector3.Scale(positCheck, mapScale) + (.5f * mapScale);
        Camera.main.transform.position = player.transform.position;
    }

    public void placeGenericRoom()
    {
        randPrefab = Random.Range(0, genericRoomPrefab.Length); 
        roomInScene = genericRoomPrefab[randPrefab]; 
        openDoors(roomInScene);
        parent(Instantiate(roomInScene, Vector3.Scale(positCheck, mapScale), Quaternion.identity));
        closeDoors(roomInScene);
    }

    public void parent(GameObject roomInScene)
    {
        var v = roomInScene.transform.position;
        roomInScene.transform.parent = this.transform;
        roomInScene.transform.localPosition = v;
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
