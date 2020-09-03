using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMechanics : MonoBehaviour
{
    public int maxJumps;
    public int jumps;
    public float jumpForce;
    public float moveSpeed;
    public float maxSpeed;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(jumps > 0)
            {
                Debug.Log("space!");
                rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                jumps -= 1;
            }
            
        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");
            if (rb.velocity.x > -maxSpeed)
            {
                rb.AddForce(-transform.right * moveSpeed, ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("right");
            if (rb.velocity.x < maxSpeed)
            {
                rb.AddForce(transform.right * moveSpeed, ForceMode2D.Impulse);
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
        jumps = maxJumps;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "pickup")
        {
            Debug.Log("pickup");
            jumpForce += 0.5f;
            maxJumps += 1;
            jumps += 1;
            Destroy(collision.gameObject);
        }
    }
}
