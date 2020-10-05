using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public LevelGeneratorScript lgs;

    private Lever lever;
    private BossDoor bossDoor;
    private bool doorShouldOpen = false;

    public void Start()
    {
        lever = GetComponentInChildren<Lever>();
        bossDoor = GetComponentInChildren<BossDoor>();
    }

    // called on room transition
    public void tryOpenBossDoor(int x, int y)
    {
        if(lever.wasTriggered && lgs.bossPosition.x == x && lgs.bossPosition.y == y)
        {
            bossDoor.Open();
        }
    }

}
