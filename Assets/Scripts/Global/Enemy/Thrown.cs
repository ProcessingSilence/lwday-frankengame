using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Thrown : StateMachineBehaviour
{
    private Vector2 thrownDireciton;

    private float velocity;
    public GameObject projectileHitbox;

    private Rigidbody2D rb;
    
    private ThrownVals ThrownVals_script;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        animator.gameObject.layer = 15;
        projectileHitbox = Instantiate(projectileHitbox,animator.transform.position, quaternion.identity);
        projectileHitbox.transform.parent = animator.gameObject.transform;
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        velocity = animator.gameObject.GetComponent<ThrownVals>().givenVelocity;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        rb.velocity = animator.transform.right * velocity;
    }
    
}
