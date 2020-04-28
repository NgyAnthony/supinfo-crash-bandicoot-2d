using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour
{
    //Drag in the Bullet Emitter from the Component Inspector.
    public GameObject Bullet_Emitter;
 
    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;
 
    //Enter the Speed of the Bullet from the Component Inspector.
    public float Bullet_Forward_Force;
    
    //Function called by animation controller
    void Shoot()
    {
        //The Bullet instantiation happens here.
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet,Bullet_Emitter.transform.position,Bullet_Emitter.transform.rotation) as GameObject;
        
        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();
        
        Vector2 move = Vector2.left;

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.AddForce(move * Bullet_Forward_Force);
 
        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(Temporary_Bullet_Handler, 1.0f);
    }
}
