using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public States currentState;
    private PlayerController playerController;
    private TakeDamage takeDamage;
    public Animator animator;

    public bool leftOrRight;
    public enum States
    {
        Normal,
        PainAnimation,
        Hurt,
        Dead,
        Holding
    }
    // Start is called before the first frame update
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        takeDamage = GetComponent<TakeDamage>();
        takeDamage.playerState = this;
        animator = GetComponent<Animator>();
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
        currentState = States.Normal;
        animator.SetInteger("state", 0);
        playerController.enabled = true;
    }
}
