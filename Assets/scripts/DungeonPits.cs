﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonPits : MonoBehaviour
{
    private Vector2 spawnPos;
    public void OnCollisionEnter2D(Collision2D c)
    {

        if (c.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var tilemap = GetComponent<Tilemap>();
            var player = c.transform.GetComponent<Player>();



            string tileType = "";
            Vector3Int tileCenter = Vector3Int.zero;

            for (int i = 0; i != c.contactCount; ++i)
            {
                var center = tilemap.layoutGrid.WorldToCell(c.GetContact(i).point + player.lastVelocity.normalized * 0.1f);
                var tile = tilemap.GetTile(center);

                if (tile != null)
                {
                    tileType = tile.name;
                    tileCenter = center;
                    break;
                }
            }

            // ??? this happens sometiems
            if (tileType == "")
            {
                return;
            }

            if (tileType.Contains("PIT"))
            {

                spawnPos = this.transform.position + new Vector3(tileCenter.x + 0.5f, tileCenter.y + 0.5f, 0);
                player.FallIntoPit(spawnPos);
                FindObjectOfType<EnvSounds>().playFallDown();
                makePitPrefab();
            }
        }
    }

    public void makePitPrefab()
    {
        var v = Instantiate(Resources.Load<GameObject>("Prefab/PitPrefab"));
        v.GetComponent<Animator>().SetInteger("PITID", Utility.level);
        v.transform.position = spawnPos;
        Destroy(v, 1.2f);
    }
}
