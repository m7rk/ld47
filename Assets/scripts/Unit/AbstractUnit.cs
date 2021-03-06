﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractUnit : MonoBehaviour
{
    [HideInInspector] public int currentHP;
    public abstract void hurt();
    public abstract int maxHealth();

    private SpriteRenderer[] sprites = null;


    public void makeProjectile(Vector2 dir, string layer, Vector3 offset)
    {
        var v = Instantiate(Resources.Load<GameObject>("Prefab/" + layer));
        v.GetComponent<Projectile>().setup(dir, layer);
        v.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
        v.layer = LayerMask.NameToLayer(layer);
        v.transform.position = this.transform.position + offset;
    }

    public void setRenderIndex()
    {
        if(sprites == null)
        {
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        int idx = 0;
        foreach(var v in sprites)
        {
            v.sortingOrder = Utility.transformToLayer(this.transform.position) - (--idx);
        }
    }

    public void toCorpseLayer()
    {
        foreach (var v in sprites)
        {
            v.sortingOrder = -60;
        }
    }
}
