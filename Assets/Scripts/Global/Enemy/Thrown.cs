using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : StateMachineBehaviour
{
    private Vector2 thrownDireciton;

    private float velocity;
    private float goreVeloctiyRequirement;

    private Rigidbody2D rb;
    
    private ThrownVals ThrownVals_script;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        ThrownVals_script = animator.gameObject.GetComponent<ThrownVals>();
        velocity = ThrownVals_script.givenVelocity;
        goreVeloctiyRequirement = ThrownVals_script.goreVelocityRequirement;
        animator.gameObject.layer = 12;
        animator.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        rb.velocity = animator.transform.right * velocity;
    }
}
