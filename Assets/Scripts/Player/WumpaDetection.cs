using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WumpaDetection : MonoBehaviour
{
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;
    
    //Reference to layers
    private int wumpasLayer;
    private int bigWumpasLayer;
    
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        wumpasLayer = LayerMask.NameToLayer("Wumpas");
        bigWumpasLayer = LayerMask.NameToLayer("BigWumpas");
    }

    private void PickWumpa(Collider2D collision)
    {
        
        //If the collided object isn't on the Wumpas layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != wumpasLayer || !playerHealth.isAlive)
            return;
        
        playerHealth.wumpasNumber += 1;

        //If the player gets 100 wumpas, reset the counter and add a life.
        if (playerHealth.wumpasNumber >= 100)
        {
            playerHealth.remainingLives += 1;
            playerHealth.wumpasNumber -= 100;
            AudioManager.PlayHealthRecoverAudio();
        }
        
        AudioManager.PlayWumpaCollectionAudio();

        //Destroy the picked wumpa.
        Destroy(collision.gameObject);
        
        //Tell the manager to show the number of wumpas
        UIManager.WumpaUI(playerHealth.wumpasNumber);
        playerHealth.refreshUI();
    }
    
    private void PickBigWumpa(Collider2D collision)
    {
        //If the collided object isn't on the BigWumpas layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != bigWumpasLayer || !playerHealth.isAlive)
            return;

        playerHealth.wumpasNumber += 10;

        //If the player gets 100 wumpas, reset the counter and add a life.
        if (playerHealth.wumpasNumber >= 100)
        {
            playerHealth.remainingLives += 1;
            playerHealth.wumpasNumber -= 100;
        }
        
        //Destroy the picked BigWumpa.
        Destroy(collision.gameObject);
        
        //Tell the manager to show the number of wumpas
        UIManager.WumpaUI(playerHealth.wumpasNumber);
        playerHealth.refreshUI();
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PickWumpa(collision);
        PickBigWumpa(collision);
    }
}
