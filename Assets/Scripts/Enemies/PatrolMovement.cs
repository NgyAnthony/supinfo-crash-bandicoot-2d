using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : PhysicsObject
{
    public float speed = 1;
    public bool moveRight;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        if (moveRight)
        {
            move.x = 1;
        }
        else
        {
            move.x = -1;
        }

        targetVelocity = move * speed;
        
    }
}
