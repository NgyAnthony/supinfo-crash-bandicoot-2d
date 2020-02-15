using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathVFXPrefab;	//The visual effects for player death
    
    private bool isAlive = true;
    private int remainingLives = 2;
    private int remainingShields = 0;
    private int wumpasNumber = 0;
    
    private int trapsLayer;
    private int wumpasLayer;
    private int shieldsLayer;
    
    // Start is called before the first frame update
    private void Start()
    {
        trapsLayer = LayerMask.NameToLayer("Traps");
        wumpasLayer = LayerMask.NameToLayer("Wumpas");
        shieldsLayer = LayerMask.NameToLayer("Shields");
    }

    private void Update()
    {
        UIManager.LivesUI(remainingLives);
    }

    private void DeathEvent()
    {
        //Instantiate the death particle effects prefab at player's location
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);

        //Disable player game object
        gameObject.SetActive(false);
    }
    
    private void DeadOrLost()
    {
        //Tell the Game Manager that the player lost and tell the Audio Manager to play
        //the lost audio
        if (remainingLives < 0 & isAlive == false)
        {
            GameManager.PlayerLost();
            // TODO AudioManager.PlayLostAudio();

            
        //Tell the Game Manager that the player died and tell the Audio Manager to play
        //the death audio
        } else if (remainingLives >= 0 & isAlive == false)
        {
            GameManager.PlayerDied();
            gameObject.transform.position = GameManager.checkpointPos;
            gameObject.SetActive(true);
            isAlive = true;
            // TODO AudioManager.PlayDeathAudio();

        }
    }
    
    private void HitTrap(Collider2D collision)
    {    
        //If the collided object isn't on the Traps layer OR if the player isn't currently
        //alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != trapsLayer || !isAlive)
            return;
        
        //Trap was hit, so set the player's alive state to false
        isAlive = false;
        remainingLives -= 1;
        
        //Kill the player.
        DeathEvent();

        //Find out if player is just dead or lost.
        DeadOrLost();
    }

    private void PickWumpa(Collider2D collision)
    {

        //If the collided object isn't on the Wumpas layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != wumpasLayer || !isAlive)
            return;
        
        //If the player gets 100 wumpas, reset the counter and add a life.
        if (wumpasNumber == 100)
        {
            remainingLives += 1;
            wumpasNumber = 0;
        }
        
        wumpasNumber += 1;
        
        //Destroy the picked wumpa.
        Destroy(gameObject);
        
        //Tell the manager to show the number of wumpas
        UIManager.WumpaUI(wumpasNumber);
    }

    private void PickShield(Collider2D collision)
    {

        //If the collided object isn't on the Shields layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != shieldsLayer || !isAlive)
            return;
        
        //If the player has less than 3 shields, add a shield.
        if (remainingShields < 3)
        {
            remainingShields += 1;
        }
        
        //Destroy the picked shield.
        Destroy(gameObject);
        
        
        //Tell the manager to show the number of shields
        UIManager.ShieldUI(remainingShields);
    }
    
    //At each collision, this function checks what it has hit and decides what it will do.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        HitTrap(collision);
        PickWumpa(collision);
        PickShield(collision);
    }

}
