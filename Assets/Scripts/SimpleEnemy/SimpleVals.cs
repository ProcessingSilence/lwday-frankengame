﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVals : MonoBehaviour
{
    public bool isWalking;
    public bool walkWhenPlayerClose;
    private bool detectedPlayer;
    public float requiredDetectionDist;
    
    public float walkSpeed;
    private Animator animator;

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (isWalking)
        {
            animator.SetBool("walking", true);
        }

        if (walkWhenPlayerClose)
        {
            player = GameObject.Find("Player").transform;
        }
    }

    void Update()
    {
        if (walkWhenPlayerClose && detectedPlayer == false)
        {
            if (Vector2.Distance(new Vector2(player.position.x ,0), new Vector2(transform.position.x, 0)) <= requiredDetectionDist)
            {
                detectedPlayer = true;

                animator.SetBool("walking", true);              
            }
        }
    }
}
 