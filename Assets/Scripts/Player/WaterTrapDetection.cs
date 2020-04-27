using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrapDetection : MonoBehaviour
{
    //Reference to water layer
    private int waterLayer;
    
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;
    
    //Reference to input control
    private PlayerPlatformerController inputControl;
    
    //Reference to animator
    public Animator animator;
    
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        inputControl = GetComponent<PlayerPlatformerController>();
        animator = GetComponent<Animator> ();
    }

    private void Start()
    {
        waterLayer = LayerMask.NameToLayer("WaterTraps");
    }

    private void HitWaterTrap(Collider2D collision)
    {    
        //If the collided object isn't on the Waters layer OR if the player isn't currently
        //alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != waterLayer || !playerHealth.isAlive)
            return;
        
        //Water was hit, so set the player's alive state to false
        playerHealth.isAlive = false;
        playerHealth.remainingLives -= 1;
        
        //Stop taking inputs
        inputControl.canControl = false;
        
        //Activate the drowning animation
        animator.SetBool("isDrowning", true);
    }
    
    //Activated in the animator
    private void DrowningIsOver()
    {
        //Stop drowning
        animator.SetBool("isDrowning", false); 
        
        //Kill the player.
        playerHealth.DeathEvent();

        //Find out if player is just dead or lost.
        playerHealth.DeadOrLost();
        
        //Give back input control
        inputControl.canControl = true;
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        HitWaterTrap(collision);
    }
}
