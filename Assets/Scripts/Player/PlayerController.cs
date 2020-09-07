// MAIN MECHANIC: A quick and consistent jump (plus multi-jump) that is NOT affected by how long space is held.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
        public bool jumpFromThrowingEnemy;
        
   
    // Movement 
        public float moveSpeed;
        private float movVec;   
        
        // Multiplies horizontal velocity to determine direction in FixedUpdate().
        [HideInInspector]
        public int inputDirection;
    
                       
    // Components
        private Rigidbody2D rb;
        private CircleCollider2D boxCollider2D;
   
        
    // Sprite
        private SpriteRenderer spriteRenderer;
        public Sprite[] spriteStates = new Sprite[4];
        private GameObject aimFace;
        public int chosenSprite; 
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<CircleCollider2D>();
        currentJumpsLeft = multiJumpLimit;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
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
            if (iJumpedFlag > 1)
            {
                iJumpedFlag = 0;
            }    
        }
        
        if (!IsGrounded())
        {
            if (currentJumpsLeft > 0 && Input.GetKeyDown(KeyCode.W))
            {
                Jump();
                currentJumpsLeft -= 1;
            }

            if (chosenSprite != 3)
            {
                chosenSprite = 2;
            }
        }

        if (jumpFromThrowingEnemy)
        {
            jumpFromThrowingEnemy = false;
            Jump();
        }

        /*
        if (IsGrounded() && iJumpedFlag > 1)
        {
            iJumpedFlag = 0;
        } 
        */           
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpVel;
    }

    void Gravity()
    {
        rb.velocity += Vector2.up * Physics2D.gravity.y * (gravityWeight - 1);
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

        if (chosenSprite != 3)
        {
            if (IsGrounded() )
            {
                // Idle
                if (inputDirection == 0)
                {
                    chosenSprite = 1;
                }
                
                // Moving
                else
                {
                    chosenSprite = 0;
                }
            }
            // Jumping
            else if (!IsGrounded())
            {
                chosenSprite = 2;
            }
        }
        spriteRenderer.sprite = spriteStates[chosenSprite];
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
        Debug.Log(other.gameObject);
    }
    
}
