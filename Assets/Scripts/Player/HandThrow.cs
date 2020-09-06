using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandThrow : MonoBehaviour
{

    public Vector2 playerPos;
    public Vector2 throwPos;

    public float throwOffset;   
    public float speed = 1;
    public float timeSpeed;
    public float throwSpeed;
    private float currentTime;
    public int throwDirection;

    // 0: No enemy.
    // Enemy found - 1: "caught" anim bool not set, 2: anim bool set; set position + rotation based on handObj.
    private int haveCaughtEnemy;

    private bool currentlyThrowing;

    private bool thrownOnceCheck;

    // Other properties    
    private PlayerController PlayerController_script;  
    private HandEnable HandEnable_script;


    
    public Transform player;
    // Direction of arm is based on player spriteRenderer flipX direction.
    private SpriteRenderer playerSpriteDirection;
    
    private GameObject playerObj;
    public GameObject caughtEnemy;

    void Start()
    {
        playerObj = player.gameObject;
        PlayerController_script = playerObj.GetComponent<PlayerController>();
        playerSpriteDirection = playerObj.GetComponent<SpriteRenderer>();
        HandEnable_script = playerObj.GetComponent<HandEnable>();  
        currentlyThrowing = true;
    }

    // Update is called once per frame
    void Update()
    {      
        Throw();
    }

    private void LateUpdate()
    {
        CaughtEnemyCheck();        
        DoneThrowing();
    }


    // Ping-pongs from player position to playerPos + offset, movement based on deltaTime.
    private void Throw()
    {
        if (thrownOnceCheck == false)
        {
            thrownOnceCheck = true;
            if (playerSpriteDirection.flipX == false)
            {
                throwDirection = 1;
            }
            else
            {
                throwDirection = -1;
            }
        }



        playerPos = player.transform.position;
        throwPos = new Vector2(playerPos.x + throwOffset * throwDirection, playerPos.y);
        currentTime = Time.time;

        if (currentlyThrowing)
        {
            timeSpeed += Time.deltaTime * throwSpeed;
            transform.position = Vector2.Lerp(player.transform.position, throwPos, Mathf.PingPong(timeSpeed, 1.0f));
        }
    }
    
    // Reset all vars and disable self, activate aiming if enemy is caught.
    private void DoneThrowing()
    {
        if (timeSpeed > 2)
        {
            HandEnable_script.caughtEnemy = caughtEnemy;
            caughtEnemy = null;
            haveCaughtEnemy = 0;
            timeSpeed = 0;
            throwDirection = 0;
            thrownOnceCheck = false;
            currentlyThrowing = true;
            gameObject.SetActive(false);
        }
    }
        
    // Upon enemy found, Set enemy to "caught" state, have it follow transform properties of the hand.
    private void CaughtEnemyCheck()
    {  
        if (haveCaughtEnemy == 1)
        {
            haveCaughtEnemy = 2;
            caughtEnemy.GetComponent<Animator>().SetBool("caught", true);
            caughtEnemy.GetComponent<Rigidbody2D>().gravityScale = 0;
            caughtEnemy.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (haveCaughtEnemy == 2)
        {
            caughtEnemy.transform.position = transform.position;
            caughtEnemy.transform.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && haveCaughtEnemy < 1)
        {
            Debug.Log("Hit Enemy");
            haveCaughtEnemy = 1;
            caughtEnemy = other.gameObject;
        }
    }
}
