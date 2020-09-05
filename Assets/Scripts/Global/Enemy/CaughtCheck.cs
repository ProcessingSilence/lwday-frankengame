using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtCheck : MonoBehaviour
{
    public int isCaught;
    public Transform hand;
    private Animator animator;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        if (isCaught == 1)
        {
            transform.position = hand.position;
            transform.rotation = hand.rotation;
        }
        if (isCaught == 2)
        {
            isCaught = 3;
            animator.SetBool("thrown", true);
        }
    }
}
