using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//source: https://www.youtube.com/watch?v=fDXtMlL2ahU

public class move : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private float boostTimer;
    private bool hitBoost;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 5;
        boostTimer = 0;
        hitBoost = false;
    }

    void FixedUpdate()
    {
        Move();
    }
    
    void Update()
    {
        Inputs();

        if (hitBoost)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                speed = 5;
                boostTimer = 0;
                hitBoost = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("speedBoost"))
        {
            hitBoost = true;
            speed = 10;
        }
    }


    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        direction = new Vector2(moveX, 0);
    }
    void Move()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
