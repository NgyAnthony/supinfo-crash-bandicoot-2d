using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyCrate : MonoBehaviour
{
    //Get prefab
    public GameObject wumpaPrefab;
    
    //Reference to animator
    private Animator animator;

    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;
    
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        animator = transform.parent.GetComponent<Animator> ();
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
    }

    void DestroyCrate(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & !AttackManager.isAttacking)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(wumpaPrefab, transform.position, transform.rotation);
            }
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
        DestroyCrate(collision);
    }
}
