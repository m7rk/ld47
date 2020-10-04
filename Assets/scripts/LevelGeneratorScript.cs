using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorScript : MonoBehaviour
{
    public int randNumbX;
    public int randNumbY;

    public int bossX;
    public int bossY;

    public int keyX;
    public int keyY;

    public int playerX;
    public int playerY;

    public bool bossroom = false;
    public bool keyroom = false;
    public bool playerroom = false;

    // Start is called before the first frame update
    void Start()
    {
        #region square selection for boss, key, player
        while (bossroom == false)
        {
            randNumbX = Random.Range(0, 3);
            randNumbY = Random.Range(0, 3);

            if (randNumbY != 1) // checking that coordinates for boss room are not in the middle row
            {
                bossX = randNumbX;
                bossY = randNumbY;
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
                    playerroom = true;
                }
            }
        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {

    }
}
