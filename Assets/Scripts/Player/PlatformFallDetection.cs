using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFallDetection : MonoBehaviour
{   
    //Reference to platform layer
    private int fallingPlatformLayer;
    
    //Platform gameobject
    private Rigidbody2D myPlatform;
    
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        fallingPlatformLayer = LayerMask.NameToLayer("FallingPlatforms");
    }

    private void DetectPlatformFall(Collider2D collision)
    {
        
        //If the collided object isn't on the Shields layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != fallingPlatformLayer || !playerHealth.isAlive)
            return;
        
        //Get the reference to the rigibody2d of the platform
        myPlatform = collision.gameObject.GetComponent<Rigidbody2D>();
        
        //Make the platform fall after 2s
        Invoke("MakePlatformFall", 2f);
    }

    private void MakePlatformFall()
    {
        //Set the rigidbody2d to dynamic to make the platform fall
        myPlatform.bodyType = RigidbodyType2D.Dynamic;
    }

    //At each collision, this function checks what it has hit and decides what it will do.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        DetectPlatformFall(collision);
    }
}
