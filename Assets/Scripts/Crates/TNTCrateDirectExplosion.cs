using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTCrateDirectExplosion : MonoBehaviour
{
    
    //Reference to animator
    private Animator animator;
    
    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        animator = GetComponent<Animator> ();
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
    }

    void TriggerBomb(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & AttackManager.isAttacking)
        {
            animator.SetBool("isBlowingUp", true);
            AudioManager.PlayTntExplosionAudio();
        }
    }
    
    void DetonationIsOver()
    { 
        animator.SetBool("isDetonated", false);
        animator.SetBool("isBlowingUp", true);
        AudioManager.PlayTntExplosionAudio();
    }

    void ExplosionIsOver()
    {
        animator.SetBool("isBlowingUp", false);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBomb(collision);
    }
}
