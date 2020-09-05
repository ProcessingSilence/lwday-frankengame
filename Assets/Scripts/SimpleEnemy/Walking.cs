using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateMachineBehaviour
{
    private float speed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private SimpleVals SimpleVals_script;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        SimpleVals_script = animator.GetComponent<SimpleVals>();
        speed = SimpleVals_script.walkSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (speed > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (speed < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}
