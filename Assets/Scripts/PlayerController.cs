﻿// MAIN MECHANIC: A quick and consistent jump (plus multi-jump) that is NOT affected by how long space is held.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private LayerMask platformLayerMask;
    
    // Jump
        //public float jumpFall_vel;

        public float jumpVel;
        public float gravityWeight = 2f;
        // How many midair jumps are allowed, set to 0 to disable.
        
        public int multiJumpLimit;
        public int currentJumpsLeft;
        
        // Gets y position of player when they are no longer grounded.
            // If the player's y-position >= yPosLimit, they will start falling
        private float yPosLimit;
        
        // Flag that determines what happens midair when not grounded.
            // 0- Currently falling
            // Have jumped: 1-  Going up, did not touch height limit. 2- Touched height limit, switch to 0.
        private int iJumpedFlag;
        
        // When pressing jump input, waits [input time] before seeing if player lands on ground, if the player lands on
        // the ground within that time period, they will automatically jump.
        private float jumpPressedPeriodCurrent;
        private float jumpPressedPeriodTime = 0.1f;
        
   
    // Movement 
        public float moveSpeed;
        private float movVec;   
        
        // Multiplies horizontal velocity to determine direction in FixedUpdate().
        private int inputDirection;
    
    
    // Fall Death
        public float fallDeathPos;
    
    
    // Components
        private Rigidbody2D rb;
        private BoxCollider2D boxCollider2D;
   
    // Sprite
        private SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        currentJumpsLeft = multiJumpLimit;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        FallDeath();
        Gravity();
    }

    private void Update()
    {
        JumpingInput();
        HorizontalMovement();
        rb.velocity = new Vector2(moveSpeed * inputDirection * Time.fixedDeltaTime, rb.velocity.y);
        SpriteRender();
    }

    
    
    void JumpingInput()
    {
        jumpPressedPeriodCurrent -= Time.deltaTime;
        
        // Reset timer on jump.
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpPressedPeriodCurrent = jumpPressedPeriodTime;
        }
        
        if (IsGrounded())
        {
            currentJumpsLeft = multiJumpLimit;
            if (rb.velocity.y <= 0 && jumpPressedPeriodCurrent > 0)
            {
                Jump();
            }
        }
        
        if (!IsGrounded() && currentJumpsLeft > 0 && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
            currentJumpsLeft -= 1;
        }

        if (IsGrounded() && iJumpedFlag > 1)
        {
            iJumpedFlag = 0;
        }            
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpVel;
    }

    void Gravity()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityWeight - 1);
    }
    
    // Pseudo-respawns player after falling to determined y value
    void FallDeath()
    {
        if (transform.position.y <= fallDeathPos)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            transform.position = new Vector3(0,0,0);
        }
    }

    private void HorizontalMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            inputDirection = 1;
        }     
        else if (Input.GetKey(KeyCode.A))
        {
            inputDirection = -1;
        }    
        else
        {
            inputDirection = 0;
        }
    }

    private void SpriteRender()
    {
        // Left
        if (inputDirection < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Right
        else if (inputDirection > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f,Vector2.down, .05f, platformLayerMask);
        return raycastHit.collider != null;
    }

    // Prevents player from auto-jumping after touching the one-way platforms while velocity is upwards.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("OneWayPlatform") && rb.velocity.y > 0)
        {
            jumpPressedPeriodCurrent = 0;
        }
    }
}
