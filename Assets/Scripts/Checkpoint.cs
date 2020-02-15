using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    private Vector2 singleCheckpoint; //Position of the checkpoint object
    private int playerLayer; //Reference to the player layer
    private SpriteRenderer FlagSprite; //Sprite of the object
    private Sprite checkedFlag; //The sprite of a checked flag
    public GameObject checkedFlagRef; //The object containing the checked flag
    
    // Start is called before the first frame update
    void Start()
    {
        singleCheckpoint = gameObject.transform.position;
        playerLayer = LayerMask.NameToLayer("Player");

        checkedFlag = checkedFlagRef.GetComponent<SpriteRenderer>().sprite;
        
        FlagSprite = GetComponent<SpriteRenderer>();
    }

    public void CollideWithPlayer(Collider2D collision)
    {
        //If the collided object isn't on the player layer, exit
        if (collision.gameObject.layer != playerLayer)
            return;
        
        if (collision.gameObject.layer == playerLayer || singleCheckpoint.x > GameManager.checkpointPos.x)
        {
            GameManager.checkpointPos = singleCheckpoint;
            FlagSprite.sprite = checkedFlag;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {  
        CollideWithPlayer(collision);
    }
}
