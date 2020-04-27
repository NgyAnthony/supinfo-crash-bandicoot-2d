using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //Reference to enemies layer
    private int enemiesLayer;
    
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;
    
    //Reference to input control
    private PlayerPlatformerController inputControl;
    
    //Reference to animator
    private Animator animator;
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        inputControl = GetComponent<PlayerPlatformerController>();
        animator = GetComponent<Animator> ();
    }

    private void Start()
    {
        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }
    
    private void HitEnemy(Collider2D collision)
    {    
        //If the collided object isn't on the Enemies layer OR if the player isn't currently
        //alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != enemiesLayer || !playerHealth.isAlive)
            return;
        
        //Remove HP or Shield
        if (playerHealth.remainingShields >= 1) {
            playerHealth.remainingShields -= 1;
            
            //Activate the hurt animation
            animator.SetBool("isHurt", true);
            playerHealth.refreshUI();
        }
        else if (playerHealth.remainingShields == 0) {
            playerHealth.isAlive = false;
            playerHealth.remainingLives -= 1;
            
            //Activate the dying animation
            animator.SetBool("isDying", true);
            
            //Stop taking inputs
            inputControl.canControl = false;
        }
    }
    
    // Function is called as a trigger in the animation sprite object
    private void HurtIsOver()
    {
        //Stop hurt animation
        animator.SetBool("isHurt", false);
    }
    
    //Activated in the animator
    private void DyingIsOver()
    {
        //Stop dying animation
        animator.SetBool("isDying", false); 
        
        //Kill the player.
        playerHealth.DeathEvent();

        //Find out if player is just dead or lost.
        playerHealth.DeadOrLost();
        
        //Give back input control
        inputControl.canControl = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        HitEnemy(collision);
    }
}
