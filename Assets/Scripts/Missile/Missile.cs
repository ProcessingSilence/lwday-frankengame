using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;

    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if (collider.enabled == false)
        {
            speed = 0;
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) > 200f)
        {
            Destroy(gameObject);
        }
    }
}
