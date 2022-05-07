using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;

    private BoxCollider2D collider;

    private bool keepMoving;
    private SpriteRenderer sr;

    public GameObject goreSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 0f, 0.86f);
    }

    private void FixedUpdate()
    {
        if (collider.enabled == false && keepMoving == false)
        {
            speed = 0;
            StartCoroutine(WaitBeforeDestroy());
        }

        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) > 200f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Land") && collider.enabled)
        {
            gameObject.layer = 13;
            speed = 0;
            sr.color = new Color(0.86f, 0.74f, 0.97f);
            StartCoroutine(WaitBeforeDestroy());
        }
    }

    private IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(goreSpawn, transform.position, quaternion.identity);
        Destroy(gameObject);
    }

    public void DestroyImmediately()
    {
        Instantiate(goreSpawn, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
