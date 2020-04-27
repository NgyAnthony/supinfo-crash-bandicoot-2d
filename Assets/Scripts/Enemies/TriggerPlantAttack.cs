using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlantAttack : MonoBehaviour
{
    
    //Reference to platform layer
    private int playerLayer;
    
    //Reference to animator
    private Animator animator;
    
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        animator = GetComponent<Animator>();
    }
    
    private void PlantAttackPlayer(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            animator.SetBool("plantIsAttacking", true);
        }
    }

    private void PlantAttackIsOver()
    {
        animator.SetBool("plantIsAttacking", false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlantAttackPlayer(collision);
    }
}
