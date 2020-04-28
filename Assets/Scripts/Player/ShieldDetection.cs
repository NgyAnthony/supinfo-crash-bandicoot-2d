using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetection : MonoBehaviour
{
    //Reference to shield layer
    private int shieldsLayer;
    
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        shieldsLayer = LayerMask.NameToLayer("Shields");
    }
    
    private void PickShield(Collider2D collision)
    {

        //If the collided object isn't on the Shields layer OR if the player isn't currently
        //alive, exit.
        if (collision.gameObject.layer != shieldsLayer || !playerHealth.isAlive)
            return;
        
        //If the player has less than 3 shields, add a shield.
        if (playerHealth.remainingShields < 3)
        {
            playerHealth.remainingShields += 1;
            
            AudioManager.PlayShieldCollectionAudio();
            //Destroy the picked shield.
            Destroy(collision.gameObject);
        }

        //Tell the manager to show the number of shields
        UIManager.ShieldUI(playerHealth.remainingShields);
    }
    
    
    //At each collision, this function checks what it has hit and decides what it will do.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        PickShield(collision);
    }
}
