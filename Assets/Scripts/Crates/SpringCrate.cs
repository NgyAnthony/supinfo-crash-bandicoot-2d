using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCrate : MonoBehaviour
{
    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;

    //Reference to player controller
    private PlayerPlatformerController playerController;

    //Reference to animator
    private Animator animator;
    
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
        playerController = GameObject.Find("Crash").GetComponent<PlayerPlatformerController>();
        animator = GetComponent<Animator> ();

    }
    
    private void MakePlayerJump(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer
            & !AttackManager.isAttacking)
        {
            playerController.springJump = true;
        }
    }

    
    void DestroyCrate(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & AttackManager.isAttacking)
        {
            animator.SetBool("isDestroyed", true);
        }
    }

    void DestroyAnimationOver()
    {
        animator.SetBool("isDestroyed", false);
        Destroy(transform.parent.gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MakePlayerJump(collision);
        DestroyCrate(collision);
    }
}
