using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isIdle : StateMachineBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SimpleVals SimpleVals_script;
    //public int caught;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        SimpleVals_script = animator.GetComponent<SimpleVals>();
    }

    /*
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if (caught == 1)
        {
            caught = 2;
            animator.SetBool("caught", true);
        }
    }
    */
}
