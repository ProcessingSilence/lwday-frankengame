using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class SunBoss : MonoBehaviour
{
    public int health;
    private int detectHealth;
    private int maxHealth;
    public GameObject missile;

    public Coroutine currentAttack;

    public GameObject player;

    public Sprite smile, laugh, bite, weird, defeated, thrown;

    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    private enum MoveTowards
    {
        Choose,
        Left,
        Player,
        Right
    }

    private MoveTowards moveTowards;

    private Missile missileScript;

    public AudioClip hurtSound;
    public AudioClip roar;
    public AudioClip teleportSound;

    public float missileFiringMovementSpeed;
    private float currentMissileFiringMovementSpeed;
    public float biteMovementSpeed;
    private float currentBiteMoveSpeed;

    private Vector3 bitePos;

    public int[] attackOrder;

    public bool beginAttack;

    public Rigidbody2D rb;

    private ParticleSystem particleSystem;

    private CircleCollider2D collider;

    public GameObject hurtHitbox;

    private Animator animator;

    private BoxCollider2D boxCollider;

    private Color colorHealth;

    private int biteAmt;

    private float colliderSize;

    private bool canFireMissile;

    float lessHealthFasterAttack()
    {
        return 1f + (1f -(float)health/(float)maxHealth);
    }

    public enum Attack
    {
        Missiles,
        Bite,

        Swoop,

        BulletHell,


        Defeated,
    }

    public Attack currentAttackState;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Awake()
    {
        originalPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectHealth =health;
        maxHealth = health;
        currentBiteMoveSpeed = biteMovementSpeed;
        bitePos = player.transform.position;
        rb = GetComponent<Rigidbody2D>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        colorHealth = spriteRenderer.color;
        collider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        colliderSize = collider.bounds.size.x/2;
        currentAttack = StartCoroutine(DelayBeforeStarting());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("health/maxhealth: " + (float)health/maxHealth);
        spriteRenderer.color = Color.Lerp(Color.white, colorHealth, (float) health/maxHealth);
        if (health < detectHealth)
        {
            detectHealth = health;
            if (health > 0)
            {
                SoundSpawner.PlaySoundObj(transform.position, hurtSound);
            }
            currentBiteMoveSpeed = 0;

        }

        if (health <= 0 && health > -999)
        {
            currentAttack = null;
            GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
            foreach (var missile in missiles)
            {
                missile.GetComponent<Missile>().DestroyImmediately();
            }
            health = -1000;
            currentAttackState = Attack.Defeated;
            spriteRenderer.sprite = defeated;
            gameObject.layer = 13;
            gameObject.tag = "Enemy";
            animator.enabled = true;
            hurtHitbox.SetActive(false);
            collider.radius = 1.44f;
            collider.offset = new Vector2(0, 0.66f);
            gameObject.GetComponent<SimpleVals>().enabled = true;
            gameObject.GetComponent<ThrownVals>().enabled = true;
            collider.enabled = false;
            boxCollider.enabled = true;
            rb.gravityScale = 1;
            SoundSpawner.PlaySoundObj(transform.position, hurtSound, 1, false, 80, 0.8f);
            SoundSpawner.PlaySoundObj(transform.position, hurtSound, 1, false, 80, 0.8f);

        }

        if (currentAttack == null && health > 0)
        {
            currentAttackState = (Attack) Random.Range(0, 2);
            transform.rotation = quaternion.Euler(0,0,0);
            ResetRbTransform();
        }

        switch (currentAttackState)
        {
            case Attack.Missiles:
            {
                trailRenderer.enabled = false;
                if (currentAttack == null && health > 0)
                {
                    moveTowards = MoveTowards.Choose;
                    SoundSpawner.PlaySoundObj(transform.position, teleportSound);
                    particleSystem.Play();
                    transform.position = new Vector2(player.transform.position.x + Random.Range(-10,10f), player.transform.position.y + 8f);
                    biteAmt = 0;
                    currentAttack = StartCoroutine(FiringAtPlayer());
                }
                float step =  (missileFiringMovementSpeed * lessHealthFasterAttack()) * Time.deltaTime;
                Vector3 chosenPos = Vector3.zero;
                switch (moveTowards)
                {
                    case MoveTowards.Choose:
                    {
                        moveTowards = (MoveTowards) Random.Range(1, 4);
                        break;
                    }
                    case MoveTowards.Left:
                    {
                        chosenPos = new Vector3(-32, Random.Range(-4f,2f));
                        break;
                    }
                    case MoveTowards.Right:
                    {
                        chosenPos = new Vector3(43, Random.Range(-4f,2f));
                        break;
                    }
                    case MoveTowards.Player:
                    {
                        chosenPos = new Vector3(player.transform.position.x, Random.Range(-4f, 2f));
                        break;
                    }
                }
                transform.position =
                    Vector2.MoveTowards(transform.position, chosenPos, step);
                if (transform.position.x < -30)
                {
                    transform.position = new Vector3(player.transform.position.x, 0) + new Vector3(Random.Range(0, 10f), 0);
                    SoundSpawner.PlaySoundObj(transform.position, teleportSound);
                    particleSystem.Play();
                    if (Random.value > 0.5f)
                    {
                        moveTowards = MoveTowards.Right;
                    }
                    else
                    {
                        moveTowards = MoveTowards.Player;
                    }


                }
                if (transform.position.x > 41)
                {
                    transform.position = new Vector3(player.transform.position.x, 0) + new Vector3(Random.Range(-10f, 0f), 0);
                    SoundSpawner.PlaySoundObj(transform.position, teleportSound);
                    particleSystem.Play();
                    if (Random.value > 0.5f)
                    {
                        moveTowards = MoveTowards.Left;
                    }
                    else
                    {
                        moveTowards = MoveTowards.Player;
                    }
                }
                transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * (lessHealthFasterAttack() * 6.5f)) * lessHealthFasterAttack()));
                if (transform.position.y > 0)
                {
                    spriteRenderer.sprite = laugh;
                    if (canFireMissile)
                    {
                        canFireMissile = false;
                        MissileFire();
                    }
                }
                if (transform.position.y <= 0)
                {
                    spriteRenderer.sprite = smile;
                    canFireMissile = true;
                }

                break;
            }
            case Attack.Bite:
            {
                if (currentAttack == null  && health > 0)
                {
                    currentAttack = StartCoroutine(Bite());
                }
                var step =  currentBiteMoveSpeed * Time.deltaTime;
                transform.position =
                    Vector2.MoveTowards(transform.position, bitePos, step);
                break;
            }
            case Attack.Defeated:
            {
                spriteRenderer.sprite = defeated;
                trailRenderer.enabled = false;
                currentAttack = null;
                break;
            }
        }
    }

    IEnumerator FiringAtPlayer()
    {
        for (int i = 0; i < Random.Range(7-(lessHealthFasterAttack() * 2), 9 - (lessHealthFasterAttack() * 2)); i++)
        {
            if (health > 0)
            {
                yield return new WaitForSecondsRealtime(0.4f);
                //spriteRenderer.sprite = laugh;
                //MissileFire();
                yield return new WaitForSecondsRealtime(0.2f);
                //spriteRenderer.sprite = smile;
            }
        }

        currentAttack = null;
    }

    void MissileFire()
    {
        var newMissile = Instantiate(missile, transform.position + new Vector3(Random.Range(-colliderSize, colliderSize), Random.Range(-colliderSize, colliderSize), 0), Quaternion.identity);
        Vector3 dir =
            new Vector3(player.transform.position.x + Random.Range(-3, 3), player.transform.position.y,
                player.transform.position.z) - newMissile.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        newMissile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        SoundSpawner.PlaySoundObj(transform.position, Resources.Load<AudioClip>("Audio/sunFire"), 1, false, 80, 0.8f);
    }

    IEnumerator BeginningFiringAtPlayer()
    {
        for (int i = 0; i < Random.Range(7-(lessHealthFasterAttack() * 2), 9 - lessHealthFasterAttack()); i++)
        {
            if (health > 0)
            {
                yield return new WaitForSecondsRealtime(0.4f);
                spriteRenderer.sprite = laugh;
                MissileFire();
                yield return new WaitForSecondsRealtime(0.2f);
                spriteRenderer.sprite = smile;
            }
        }

        currentAttack = null;
    }

    IEnumerator Bite()
    {
        for (int i = 0; i < Random.Range(3,4); i++)
        {
            trailRenderer.enabled = false;
            if (health > 0 && biteAmt <= 9)
            {
                currentBiteMoveSpeed = 0;
                spriteRenderer.sprite = weird;
                ResetRbTransform();
                RandomTeleportLocation();

                yield return new WaitForSecondsRealtime(0.5f);
                if (health <= maxHealth / 2f)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        // Juke the player
                        if (Random.Range(0, 6) < 3)
                        {
                            ResetRbTransform();
                            RandomTeleportLocation();
                            biteAmt++;
                            yield return new WaitForSecondsRealtime(0.5f);
                        }
                        else
                        {
                            i = 99;
                        }
                    }

                }

                bitePos = player.transform.position;
                SoundSpawner.PlaySoundObj(transform.position, roar);
                trailRenderer.enabled = true;


                currentBiteMoveSpeed = biteMovementSpeed;




                spriteRenderer.sprite = bite;

                yield return new WaitForSecondsRealtime(0.4f);
                biteAmt++;
            }
            // Prevent the sun from biting too much.
            if (biteAmt > 9)
            {
                i = 90;
                currentAttackState = (Attack) 0;
            }

        }

        currentAttack = null;

    }

    void RandomTeleportLocation()
    {
        var randomFloatX = (Random.Range(7f, 9f));
        if (Random.value > 0.5f)
        {
            randomFloatX = -randomFloatX;
        }
        var randomFloatY = (Random.Range(3f, 5f));
        if (Random.value > 0.5f)
        {
            randomFloatY = -randomFloatY;
        }
        SoundSpawner.PlaySoundObj(transform.position, teleportSound);
        particleSystem.Play();
        transform.position = new Vector2(player.transform.position.x + randomFloatX, player.transform.position.y + randomFloatY);
    }

    IEnumerator DelayBeforeStarting()
    {
        rb.velocity = new Vector2(0,1);
        yield return new WaitForSecondsRealtime(3.3f);
        rb.velocity = new Vector2(0,0);
        spriteRenderer.sprite = bite;
        yield return new WaitForSecondsRealtime(0.25f);
        spriteRenderer.sprite = smile;
        SoundSpawner.PlaySoundObj(transform.position, teleportSound);
        particleSystem.Play();
        transform.position = new Vector2(player.transform.position.x + Random.Range(0,5f), player.transform.position.y + 8f);
        currentAttack = StartCoroutine(BeginningFiringAtPlayer());
    }

    void ResetRbTransform()
    {
        transform.rotation = quaternion.Euler(0,0,0);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        rb.angularVelocity = 0;
    }
}
