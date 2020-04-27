using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDetection : MonoBehaviour
{
    //PlayerHealth gameobject
    private PlayerHealth playerHealth;
    
    //Reference to layers
    private int trapsLayer;
    
    //Reference to animator
    public Animator animator;

    //Reference to input control
    private PlayerPlatformerController inputControl;
    
    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        inputControl = GetComponent<PlayerPlatformerController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {        
        trapsLayer = LayerMask.NameToLayer("Traps");
    }
    
    private void HitTrap(Collider2D collision)
    {    
        //If the collided object isn't on the Traps layer OR if the player isn't currently
        //alive, exit. This is more efficient than string comparisons using Tags
        if (collision.gameObject.layer != trapsLayer || !playerHealth.isAlive)
            return;
        
        //Activate the dying animation
        animator.SetBool("isDying", true);

        //Trap was hit, so set the player's alive state to false
        playerHealth.isAlive = false;
        playerHealth.remainingLives -= 1;
        
        //Stop taking inputs
        inputControl.canControl = false;
    }
    
    //DyingIsOver called, code in EnemeyDamage


    //At each collision, this function checks what it has hit and decides what it will do.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        HitTrap(collision);
    }
}
