using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; // how fast the player goes
    private float horizontal; // controls horizontal axis
    private Rigidbody2D MyRb;

    // Start is called before the first frame update
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>(); //connect ref to rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        // gives the player horizontal movement
        horizontal = Input.GetAxisRaw("Horizontal");
        MyRb.velocity = new Vector2(horizontal * speed, MyRb.velocity.y);
        
    }
}
