using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{   
    //The visual effects for player death
    public GameObject deathVFXPrefab;	
    
    // Reference to ground layer
    private int groundLayer;

    //Reference to platform layer
    private int playerLayer;
    
    //Platform gameobject
    private Rigidbody2D myPlatform;     

    //PlayerHealth gameobject
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.Find("Crash").GetComponent<PlayerHealth>();
        myPlatform = gameObject.GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Platforms");
    }
    
    public void MakePlatformFall(Collider2D collision)
    {
        //If the collided object isn't on the Player layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != playerLayer || !playerHealth.isAlive)
            return;
        
        Invoke("SetToDynamic", 2f);
    }

    private void SetToDynamic()
    {
        //Set the rigidbody2d to dynamic to make the platform fall
        myPlatform.bodyType = RigidbodyType2D.Dynamic;
    }
    
    public void DestroyOnGround(Collider2D collision)
    {
        
        //If the collided object isn't on the ground layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != groundLayer || !playerHealth.isAlive)
            return;
        
        //Add VFX when the platform hits the ground
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        
        //Destroy the gameobject
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MakePlatformFall(collision);
        DestroyOnGround(collision);
    }
}
