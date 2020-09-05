using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulledIn : StateMachineBehaviour
{
    public Transform pullPos;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        pullPos = GameObject.FindGameObjectWithTag("PullPos").transform;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, pullPos.position, speed * Time.deltaTime);
        if (Vector2.Distance(animator.transform.position, pullPos.position) < 1)
        {
            animator.SetBool("isPulled", false);
            animator.SetBool("caught", true);
        }
    }
}
