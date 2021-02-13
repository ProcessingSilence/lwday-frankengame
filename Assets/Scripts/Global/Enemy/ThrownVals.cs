using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownVals : MonoBehaviour
{
    public float goreVelocityRequirement;

    public float givenVelocity;
    public Sprite deathSprite;

    public Vector2 thrownDirection;

    public bool instaKill;

    public GameObject projectileHitbox;

    public GameObject toGoreHitbox;

    public Sprite caughtSprite;

    private Animator animator;

    private SpriteRenderer sR;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }

    public void WaitUntilExploding()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSecondsRealtime(1f);
        instaKill = true;
    }
}
