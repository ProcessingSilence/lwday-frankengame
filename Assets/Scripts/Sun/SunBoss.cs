using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class SunBoss : MonoBehaviour
{
    public int health;
    private int detectHealth;
    public GameObject missile;

    public Coroutine currentAttack;

    public GameObject player;

    public Sprite smile, laugh, bite, weird, defeated, thrown;

    private SpriteRenderer spriteRenderer;

    public AudioClip hurtSound;
    public AudioClip roar;
    public AudioClip teleportSound;

    public float missileFiringMovementSpeed;
    public float biteMovementSpeed;
    private float currentBiteMoveSpeed;

    private Vector3 bitePos;

    public int[] attackOrder;

    public bool beginAttack;

    public Rigidbody2D rb;

    private ParticleSystem particleSystem;

    private CircleCollider2D collider;

    public GameObject hurtHitbox;
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
        detectHealth = health;
        currentBiteMoveSpeed = biteMovementSpeed;
        bitePos = player.transform.position;
        rb = GetComponent<Rigidbody2D>();
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        collider = GetComponent<CircleCollider2D>();        
        currentAttack = StartCoroutine(DelayBeforeStarting());
    }

    // Update is called once per frame
    void Update()
    {
        if (health < detectHealth)
        {
            detectHealth = health;
            SoundSpawner.PlaySoundObj(transform.position, hurtSound);
            currentBiteMoveSpeed = 0;
        }

        if (health <= 0 && health > -999)
        {
            currentAttack = null;
            health = -1000;
            currentAttackState = Attack.Defeated;
            spriteRenderer.sprite = defeated;
            gameObject.layer = 13;
            
            hurtHitbox.SetActive(false);
            collider.radius = 1.44f;
            collider.offset = new Vector2(0, 0.66f);
            rb.gravityScale = 1;

        }

        if (currentAttack == null && health > 0)
        {
            currentAttackState = (Attack) Random.Range(0, 2);
        }

        switch (currentAttackState)
        {
            case Attack.Missiles:
            {
                if (currentAttack == null && health > 0)
                {
                    SoundSpawner.PlaySoundObj(transform.position, teleportSound);
                    particleSystem.Play();
                    transform.position = new Vector2(player.transform.position.x + Random.Range(0,5f), player.transform.position.y + 8f);
                    currentAttack = StartCoroutine(FiringAtPlayer());
                }
                var step =  missileFiringMovementSpeed * Time.deltaTime;
                transform.position =
                    Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, 0), step);
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
                currentAttack = null;
                break;
            }
        }
    }

    IEnumerator FiringAtPlayer()
    {
        for (int i = 0; i < Random.Range(7, 9); i++)
        {
            yield return new WaitForSecondsRealtime(Random.Range(0.2f, 0.4f));
            spriteRenderer.sprite = laugh;
            var newMissile = Instantiate(missile, transform.position, Quaternion.identity);
            Vector3 dir = new Vector3(player.transform.position.x + Random.Range(-1,1), player.transform.position.y + Random.Range(-1,1), player.transform.position.z) - newMissile.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newMissile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            yield return new WaitForSecondsRealtime(0.2f);
            spriteRenderer.sprite = smile;
        }

        currentAttack = null;
    }

    IEnumerator Bite()
    {
        for (int i = 0; i < Random.Range(3,4); i++)
        {

            if (health > 0)
            {
                currentBiteMoveSpeed = 0;
                spriteRenderer.sprite = weird;
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
                yield return new WaitForSecondsRealtime(0.5f);
                bitePos = player.transform.position;
                SoundSpawner.PlaySoundObj(transform.position, roar);
                currentBiteMoveSpeed = biteMovementSpeed;
                spriteRenderer.sprite = bite;
                yield return new WaitForSecondsRealtime(0.4f);
            }


        }
        
        currentAttack = null;

    }

    IEnumerator DelayBeforeStarting()
    {
        rb.velocity = new Vector2(0,1);
        yield return new WaitForSecondsRealtime(3.3f);
        rb.velocity = new Vector2(0,0);
        spriteRenderer.sprite = smile;
        SoundSpawner.PlaySoundObj(transform.position, teleportSound);
        particleSystem.Play();
        transform.position = new Vector2(player.transform.position.x + Random.Range(0,5f), player.transform.position.y + 8f);
        currentAttack = StartCoroutine(FiringAtPlayer());
    }
}
