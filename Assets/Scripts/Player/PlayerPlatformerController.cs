using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    public float walkSpeed = 8;
    public float maxSpeed = 15;
    public float jumpTakeOffSpeed = 15;
    public bool canControl = true;
    public bool attackJumpRebound = false;
    
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();    
        animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        if (canControl)
        {
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis ("Horizontal");
            if (attackJumpRebound)
            {
                velocity.y = 10;
            }
            else if ((Input.GetButtonDown("Jump") && grounded)) {
                velocity.y = jumpTakeOffSpeed;
            } else if (Input.GetButtonUp ("Jump")) 
            {
                if (velocity.y > 0) {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        
            bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
            if (flipSprite) 
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }

            attackJumpRebound = false;
            animator.SetBool ("isOnGround", grounded);
            animator.SetFloat ("speed", Mathf.Abs (velocity.x) / maxSpeed);
        
            // If the running button is pressed...
            if (Input.GetButton("Run"))
            {
                targetVelocity = move * maxSpeed; //the velocity is at its maximum...
            }
            else
            {
                targetVelocity = move * walkSpeed; //...else the velocity is at its walking speed.
            }
        }
    }
}