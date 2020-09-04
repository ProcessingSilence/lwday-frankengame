using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMechanics : MonoBehaviour
{
    public int maxJumps;
    public int jumpsLeft;
    public float jumpForce;
    public float moveSpeed;
    public float maxSpeed;
    private Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jumping();
    }

    void HorizontalMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (rb.velocity.x > -maxSpeed)
            {
                rb.AddForce(-transform.right * moveSpeed, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (rb.velocity.x < maxSpeed)
            {
                rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void Jumping()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumpsLeft > 0)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumpsLeft -= 1;
            }           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpsLeft = maxJumps;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("pickup"))
        {
            jumpForce += 0.5f;
            maxJumps += 1;
            jumpsLeft += 1;
            Destroy(collision.gameObject);
        }
    }
    
}


