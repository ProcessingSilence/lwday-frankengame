using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpiked : StateMachineBehaviour
{
    public Sprite deSpiked;
    private Rigidbody2D rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.tag = "Enemy";
        animator.gameObject.GetComponent<SpriteRenderer>().sprite = deSpiked;
        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        animator.gameObject.GetComponent<CircleCollider2D>().offset = Vector2.zero;
        animator.gameObject.AddComponent<SpikeySlowDownVelocity>();
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

public class SpikeySlowDownVelocity : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        float minusIteration = rb.velocity.x / 10;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            rb.velocity -= new Vector2(minusIteration,0);
        }
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
}
