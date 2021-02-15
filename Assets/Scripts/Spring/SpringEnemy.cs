 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringEnemy : MonoBehaviour
{
    public bool isWalking;
    public bool walkWhenPlayerClose;
    public float requiredDetectionDist;
    
    public float walkSpeed;
    private Animator animator;

    private Transform player;

    public Sprite[] springSprites;
    private SpriteRenderer spriteRenderer;
    private bool grounded;
    private Rigidbody2D rb;
    public enum springState
    {
        Grounded,
        Preparing,
        Jumping
    }

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        /*
        if (isWalking)
        {
            animator.SetBool("walking", true);
        }
        */

         player = GameObject.Find("Player").transform;

         if (walkWhenPlayerClose == false)
         {
             StartCoroutine(Neutral());
         }
    }


    void Update()
    {
        if (walkWhenPlayerClose)
        {
            if (grounded)
                rb.velocity = Vector2.zero;
            float distance = Vector2.Distance(player.position, transform.position);
            if (distance < requiredDetectionDist)
            {
                Debug.Log("distance: " + distance + " less than: " + requiredDetectionDist);
                walkWhenPlayerClose = false;
                StartCoroutine(Neutral());
            }
        }

        spriteRenderer.flipX = leftOrRightFromPlayer() == 1;
    }
    

    IEnumerator Neutral()
    {
        yield return new WaitUntil(()=>grounded);
        rb.velocity = Vector2.zero;
        StartCoroutine(Preparing());

    }

    IEnumerator Preparing()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        spriteRenderer.sprite = springSprites[1];
        yield return new WaitForSecondsRealtime(0.25f);
        StartCoroutine(Jumping(leftOrRightFromPlayer()));
    }

    IEnumerator Jumping(int leftOrRight)
    {
        spriteRenderer.sprite = springSprites[2];
        gameObject.tag = "SpikeyEnemy";
        rb.AddForce(new Vector2(700*leftOrRight,1500));
        yield return new WaitForSecondsRealtime(0.3f);
        yield return new WaitUntil(()=>grounded);
        spriteRenderer.sprite = springSprites[1];
        yield return new WaitForSecondsRealtime(0.1f);
        spriteRenderer.sprite = springSprites[0];
        gameObject.tag = "DamageEnemy";
        StartCoroutine(Neutral());

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            grounded = false;
        }
    }

    private int leftOrRightFromPlayer()
    {        
        return player.position.x > transform.position.x ? 1 : -1;
    }
}
 