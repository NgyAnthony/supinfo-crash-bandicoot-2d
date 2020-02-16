using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroy : MonoBehaviour
{
    public GameObject deathVFXPrefab;	//The visual effects for player death
    private int groundLayer;

    private void Start()
    {
        groundLayer = LayerMask.NameToLayer("Platforms");
    }


    public void DestroyOnGround(Collider2D collision)
    {
        
        //If the collided object isn't on the Shields layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != groundLayer)
            return;
        
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyOnGround(collision);
    }
}
