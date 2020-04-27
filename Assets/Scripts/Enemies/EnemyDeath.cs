using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject deathVFXPrefab;	//The visual effects for enemy death

    //Reference to platform layer
    private int playerLayer;
    
    //Reference to tornado attack
    private TornadoAttack AttackManager;
    
    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        AttackManager = GameObject.Find("Crash").GetComponent<TornadoAttack>();
    }

    void DestoryEnemy(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer & AttackManager.isAttacking)
        {
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        DestoryEnemy(collision);
    }
}
