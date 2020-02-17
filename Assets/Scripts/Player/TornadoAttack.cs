using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoAttack : MonoBehaviour
{
    public bool isAttacking = false;
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator> ();
    }

    private void Update()
    {
        Attack();
    }

    void Attack()
    {
        //On button pressed
        if (Input.GetButton("Attack"))
        {
            isAttacking = true; //Attack status is true
            animator.SetBool("isAttacking", true); //Start attack animation
        }
        
        //On button release
        if (Input.GetButtonUp("Attack"))
        {
            isAttacking = false; //Not attacking anymore
            animator.SetBool("isAttacking", false); //Stop attack animation
        }
    }
}
