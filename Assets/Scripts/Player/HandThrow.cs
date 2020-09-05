using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandThrow : MonoBehaviour
{
    public Transform player;
    public Vector2 throwPos;

    public float speed = 1;

    public float timeSpeed;

    private bool haveCaughtEnemy;

    public GameObject caughtEnemy;

    private bool currentlyThrowing;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyThrowing)
        {
            Throw();
        }
    }

    private void Throw()
    {
        timeSpeed += Time.deltaTime * speed;
        transform.position = Vector2.Lerp(player.position, throwPos, Mathf.PingPong(timeSpeed, 1.0f));
        if (timeSpeed > 2)
        {
            timeSpeed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && haveCaughtEnemy)
        {
            caughtEnemy = other.gameObject;
        }
    }
}
