using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkuCrate : MonoBehaviour
{
    //Get prefab
    public GameObject featherPrefab;
    
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

    void DestroyCrate(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & AttackManager.isAttacking)
        {
            Instantiate(featherPrefab, transform.position, transform.rotation);
            animator.SetBool("isDestroyed", true);
            AudioManager.PlayBreakingCrateAudio();

        }
    }

    void DestroyAnimationOver()
    {
        animator.SetBool("isDestroyed", false);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyCrate(collision);
    }
}
