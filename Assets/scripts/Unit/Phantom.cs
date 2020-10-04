using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phantom : AbstractPlayerCharacter
{
    RoomManager rtl;
    public Color normal;
    public Color fire;
    public SpriteRenderer[] renderers;

    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();
    }
    Color colorLerp(Color a, Color b, float f)
    {
        return new Color(
             Mathf.Lerp(a.r, b.r, f),
             Mathf.Lerp(a.g, b.g, f),
             Mathf.Lerp(a.b, b.b, f),
             Mathf.Lerp(a.a, b.a, f)
            );
    }

    void setColor()
    {
        Color newCol = normal;

        if (shootTimer > shootTimerFrame)
        {
            newCol = colorLerp(fire, normal, (shootTimer - shootTimerFrame) / (shootTimerMax - shootTimerFrame));
        }
        foreach (var r in renderers)
        {
            r.color = newCol;
        }
    }

    

    public void Input(AbstractPlayerCharacter.CharacterMove move)
    {
        if(rtl == null)
        {
            rtl = FindObjectOfType<RoomManager>();
        }

        Vector2 delta = new Vector2(transform.position.x, transform.position.y) - rtl.addOffsetToRoom(move.location);

        if (move.didFire)
        {
            startFire();
        }

        this.transform.position = rtl.addOffsetToRoom(move.location);
            
        if (delta.x < 0)
        {
            lookingLeft = false;
        }

        if (delta.x > 0)
        {
            lookingLeft = true;
        }

        if (delta != Vector2.zero)
        {
            projectileLaunchDirection = -delta;
        }

        animateLizard(delta.magnitude < 0.01);

    }
    // Update is called once per frame
    void Update()
    {
        checkForShoot("PhantomProjectile");
        setColor();
    }

    public override void hurt()
    {
        // lol
    }

    public override int maxHealth()
    {
        return 1;
    }
}
