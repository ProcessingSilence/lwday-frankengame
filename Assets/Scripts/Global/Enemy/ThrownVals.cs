using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownVals : MonoBehaviour
{
    public float goreVelocityRequirement;

    public float givenVelocity;
    public bool beginMovement;
    public Sprite deathSprite;

    public Vector2 thrownDirection;

    public bool instaKill;

    public GameObject projectileHitbox;

    public GameObject toGoreHitbox;

    public Sprite caughtSprite;

    private Animator animator;

    private SpriteRenderer sR;

    private Rigidbody2D rb;

    public bool regularExplosion;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void WaitUntilExploding()
    {
        StartCoroutine(Countdown());
    }

    private void FixedUpdate()
    {
        if (beginMovement)
        {

        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSecondsRealtime(1f);
        instaKill = true;
    }
}
