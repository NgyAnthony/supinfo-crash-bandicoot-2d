using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroExplode : MonoBehaviour
{
    
    //Reference to animator
    private Animator animator;
    
    //Reference to platform layer
    private int playerLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        animator = GetComponent<Animator> ();
    }

    void TriggerBomb(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            animator.SetBool("isExploading", true);
            AudioManager.PlayTntExplosionAudio();
        }
    }

    void ExplosionIsOver()
    {
        animator.SetBool("isExploading", false);
        Destroy(gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerBomb(collision);
    }
}
