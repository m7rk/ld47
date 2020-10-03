using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for player, enemy, phantom.
public abstract class AbstractCharacter : MonoBehaviour
{
    public static float VEL_STOP_THRESH = 0.1f;
    public Rigidbody2D rb;
    const float ACCEL_MULT = 100f;
    const float MAX_VEL = 3f;

    public AudioSource fireSound;
    public AudioSource hurtSound;

    // which way should projectiles be shot?
    protected Vector2 projectileLaunchDirection = Vector2.right;

    // Enum for action that could be taken in any given frame.
    public enum CharacterMove
    {
        NONE,
        FIRE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
        UPLEFT,
        UPRIGHT,
        DOWNLEFT,
        DOWNRIGHT
    };

    
    public Animator sprite;

    public bool lookingLeft;

    public int hp = 3;

    public abstract void hurt();

    protected void animateLizard(Vector2 velocity)
    {
        // idle
        if (velocity.magnitude < VEL_STOP_THRESH)
        {
            sprite.ResetTrigger("walk_right");
            sprite.ResetTrigger("walk_left");
            sprite.SetTrigger("idle");
            return;
        }

        // not idle.
        if (!lookingLeft)
        {
            sprite.ResetTrigger("walk_left");
            sprite.ResetTrigger("idle");
            sprite.SetTrigger("walk_right");
        }
        else
        {
            sprite.ResetTrigger("walk_right");
            sprite.ResetTrigger("idle");
            sprite.SetTrigger("walk_left");
        }
    }

    public void tryFire(Vector2 dir, string layer)
    {
        var v = Instantiate(Resources.Load<GameObject>("Prefab/fireball"));
        v.GetComponent<Projectile>().setup(dir, "PlayerProjectile");
        v.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));
        v.layer = LayerMask.NameToLayer(layer);
        v.transform.position = this.transform.position;
        fireSound.Play();
    }

    protected void applyForcesToRigidBody(Vector2 moveVector, float delta)
    {
        rb.AddForce(moveVector * ACCEL_MULT * delta, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > MAX_VEL)
        {
            rb.velocity = rb.velocity.normalized * MAX_VEL;
        }

    }

}


