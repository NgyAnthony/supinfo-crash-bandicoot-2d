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
        singleCheckpoint = gameObject.transform.position; //Get the position of the checkpoint flag
        playerLayer = LayerMask.NameToLayer("Player"); //Get the reference layer of the player

        checkedFlag = checkedFlagRef.GetComponent<SpriteRenderer>().sprite; //Get the reference of the green/checked flag
        
        FlagSprite = GetComponent<SpriteRenderer>(); //Take the sprite of the flag(unchecked)
    }

    public void CollideWithPlayer(Collider2D collision)
    {
        //If the collided object isn't on the player layer, exit
        if (collision.gameObject.layer != playerLayer)
            return;
        
        //Check if the flag is the furtherest and if a player touched it, if so change the position of the checkpoint
        //and set the sprite to the green flag.
        if (collision.gameObject.layer == playerLayer & singleCheckpoint.x > GameManager.checkpointPos.x)
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
