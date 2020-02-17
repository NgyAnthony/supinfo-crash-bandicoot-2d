using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashCrate : MonoBehaviour
{
    //Reference to animator
    private Animator animator;

    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;
    
    //Reference to playerhealth
    private PlayerHealth playerHealth;
    
    private void Awake()
    {
        Debug.Log("I'm awake");
        playerLayer = LayerMask.NameToLayer("Player");
        animator = GetComponent<Animator> ();
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
        playerHealth = GameObject.Find("Crash").GetComponent<PlayerHealth>();
    }

    void DestroyCrate(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & AttackManager.isAttacking)
        {
            Debug.Log("Got destroyed");
            animator.SetBool("isDestroyed", true);
        }
    }

    void DestroyAnimationOver()
    {
        Debug.Log("AnimationOver");
        animator.SetBool("isDestroyed", false);
        playerHealth.remainingLives += 1;
        playerHealth.refreshUI();
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyCrate(collision);
    }
}
