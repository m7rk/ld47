using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        // Something hit this tile, deal accordingly.

        
        if (c.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var tilemap = GetComponent<Tilemap>();
            var player = c.transform.GetComponent<Player>();



            string tileType = "";
            for (int i = 0; i != c.contactCount; ++i)
            {
                var tile = tilemap.GetTile(tilemap.layoutGrid.WorldToCell(c.GetContact(i).point + player.lastVelocity.normalized * 0.1f));

                if (tile != null)
                {
                    tileType = tile.name;
                    break;
                }
            }
            
            if(tileType == "")
            {
                Debug.LogError("No luck finding tile type");
            }

            if (tileType == "fire")
            {
                player.hurt();
            }
        }
    
    }
}
