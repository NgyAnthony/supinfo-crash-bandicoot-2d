using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRebound : MonoBehaviour
{
    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;

    //Reference to playerhealth
    private PlayerHealth playerHealth;
    
    //Reference to player controller
    private PlayerPlatformerController playerController;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
        playerHealth = GameObject.Find("Crash").GetComponent<PlayerHealth>();
        playerController = GameObject.Find("Crash").GetComponent<PlayerPlatformerController>();
    }
    
    private void MakePlayerJump(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer
            & !AttackManager.isAttacking
            & playerHealth.remainingShields >= 1)
        {
            playerController.attackJumpRebound = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        MakePlayerJump(collision);
    }
}
