using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathVFXPrefab;	//The visual effects for player death
    
    public bool isAlive = true;
    
    //Information on the player
    public int remainingLives = 2;
    public int remainingShields = 0;
    public int wumpasNumber = 0;

    // Start is called before the first frame update
    private void Start()
    {
        refreshUI();
    }

    public void refreshUI()
    {
        //Tell UImanager the number of remaining lives
        UIManager.LivesUI(remainingLives);
        UIManager.ShieldUI(remainingShields);
    }
    
    public void DeathEvent()
    {
        //Instantiate the death particle effects prefab at player's location
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);

        //Disable player game object
        gameObject.SetActive(false);
        
    }
    
    public void DeadOrLost()
    {
        //Tell the Game Manager that the player lost and tell the Audio Manager to play
        //the lost audio
        if (remainingLives <= 0 & isAlive == false)
        {
            GameManager.PlayerLost();
            refreshUI();
            // TODO AudioManager.PlayLostAudio();

            
        //Tell the Game Manager that the player died and tell the Audio Manager to play
        //the death audio
        } else if (remainingLives > 0 & isAlive == false)
        {
            GameManager.PlayerDied();
            gameObject.transform.position = GameManager.checkpointPos;
            isAlive = true;
            gameObject.SetActive(true);
            refreshUI();
            // TODO AudioManager.PlayDeathAudio();
        }
    }
}
