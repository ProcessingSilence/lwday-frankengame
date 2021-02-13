using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public States currentState;
    private PlayerController playerController;
    public TakeDamage takeDamage;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public bool leftOrRight;

    private Coroutine invulnerableHurt;
    public enum States
    {
        Normal,
        PainAnimation,
        Hurt,
        TakeDamage,
        HurtInvulnerable,
        Dead,
        Holding
    }
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.Hurt:
                currentState = States.PainAnimation;
                animator.SetInteger("state", 1);
                playerController.enabled = false;
                StartCoroutine(Hurt());
                break;               
            case States.HurtInvulnerable:
                if (invulnerableHurt == null)
                {
                    invulnerableHurt = StartCoroutine(InvulnerableHurtFlashing());
                }
                break;
        }
    }

    IEnumerator Hurt()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        int force = -10;
        if (spriteRenderer.flipX)
        {
            force = -force;
        }

        var soundObj = Instantiate(Resources.Load("Audio/SoundPlayerObj")) as GameObject;
        var soundObjAs = soundObj.GetComponent<AudioSource>();
        soundObjAs.clip = Resources.Load("Audio/hurtSound") as AudioClip;
        soundObjAs.Play();
        var rb = playerController.gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.velocity = new Vector2(force, 0);
        yield return new WaitForSecondsRealtime(0.3f);
        currentState = States.HurtInvulnerable;
        animator.SetInteger("state", 0);
        playerController.enabled = true;
    }

    IEnumerator InvulnerableHurtFlashing()
    {
        bool onOff = true;
        for (int i = 0; i < 100f; i++)
        {
            onOff = !onOff;
            spriteRenderer.enabled = onOff;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        spriteRenderer.enabled = true;
        currentState = States.Normal;
        invulnerableHurt = null;
        takeDamage.damageState = null;
    }
}
