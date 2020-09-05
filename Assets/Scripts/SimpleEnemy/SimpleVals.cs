using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVals : MonoBehaviour
{
    public bool isWalking;
    
    public float walkSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (isWalking)
        {
            animator.SetBool("walking", true);
        }
    }
}
