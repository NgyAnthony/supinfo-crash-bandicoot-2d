using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTCrateTimer : MonoBehaviour
{
    
    //Reference to animator
    private Animator animator;
    
    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;
    
    //Determine if player is in range of bomb explosion
    private bool isInRange;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        animator = transform.parent.GetComponent<Animator> ();
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
    }

    void TriggerBomb(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & !AttackManager.isAttacking)
        {
            animator.SetBool("isDetonated", true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBomb(collision);
    }
}
