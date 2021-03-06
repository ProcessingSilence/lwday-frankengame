﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Detects when enemies are hit or when buttons are pressed, uses a larger hitbox than ToGoreHitbox so it's more forgiving
// to the player to hit the objects.

public class ProjectileHitbox : MonoBehaviour
{
    private GameObject myParent;
    private GameObject goreExplosion;
    private GameObject regularExplosion;
    private ThrownVals ThrownVals_script;
    private Animator enemyAnimator;
    private GameObject otherEnemy;
    private GameObject buttonObj;
    private bool alreadyPressedButton;
    private bool goreOrCorpse;
    public Transform child;

    public bool damageTheSun;

    public Collider markedTarget;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.gameObject;
        ThrownVals_script = myParent.GetComponent<ThrownVals>();
        enemyAnimator = myParent.GetComponent<Animator>();
        goreExplosion = Resources.Load("GoreSpawner") as GameObject;
        regularExplosion = Resources.Load("Explosion") as GameObject;
        if (ThrownVals_script.givenVelocity >= ThrownVals_script.goreVelocityRequirement)
        {
            goreOrCorpse = false;
        }
        else
        {
            goreOrCorpse = true;
        }
    }

    void FixedUpdate()
    {
        PressButton();
        KillOtherEnemy();
    }

    void PressButton()
    {
        if (buttonObj && alreadyPressedButton == false)
        {
            alreadyPressedButton = true;
            StartCoroutine(ButtonPressProcess());
        }
    }

    IEnumerator ButtonPressProcess()
    {
        var Button_script = buttonObj.GetComponent<Button>();
        Button_script.buttonHit = 1;
        ToGoreHitbox goreScript = child.GetComponent<ToGoreHitbox>();
        yield return new WaitForSecondsRealtime(0.1f);
        Instantiate(Resources.Load("GoreSpawner") as GameObject, transform.position, Quaternion.identity);
        Destroy(myParent);
    }

    void KillOtherEnemy()
    {
        if (otherEnemy)
        {
            GameObject explosionType;
            if (otherEnemy.CompareTag("Missile"))
            {
                explosionType = regularExplosion;
            }
            else
            {
                explosionType = goreExplosion;
            }

            Instantiate(explosionType, otherEnemy.transform.position, Quaternion.identity);
            Destroy(otherEnemy);
            otherEnemy = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy")  || other.CompareTag("UnthrowableEnemy") || other.CompareTag("Missile")) && otherEnemy == false)
        {
            otherEnemy = other.gameObject;
        }

        if (other.CompareTag("SunHitbox"))
        {
            if (damageTheSun == false)
            {
                damageTheSun = true;
                DamageSun(other.gameObject);
            }
        }

        if (other.CompareTag("SpikeyEnemy") || other.CompareTag("DamageEnemy"))
        {
            SpikeyEnemy(other);
        }

        if (other.gameObject.layer == 16)
        {
            buttonObj = other.gameObject;
        }
    }

    void SpikeyEnemy(Collider2D other) 
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("spikey enemy");
        other.gameObject.GetComponent<Animator>().SetBool("hit", true);
        other.tag = "Enemy";
        Instantiate(Resources.Load("GoreSpawner") as GameObject, transform.position, Quaternion.identity);
        Destroy(myParent);
    }

    void DamageSun(GameObject sun)
    {
        SunBoss sunBossScript= sun.GetComponent<SunBoss>();
        sunBossScript.health -= 1;
        Debug.Log("Hit the sun");
        var goreExplosion = Instantiate(Resources.Load("GoreSpawner") as GameObject, sun.transform.position, Quaternion.identity);
        goreExplosion.GetComponent<GoreExplosion>().goreAmount = 0;
        if (transform.parent.CompareTag("Missile"))
        {
            Instantiate(regularExplosion, transform.position, Quaternion.identity);
        }

        var sunRb = sun.GetComponent<Rigidbody2D>();
        if (sunBossScript.currentAttackState == SunBoss.Attack.Bite && sunBossScript.health > 0)
        {
            var myVelocity = transform.parent.GetComponent<Rigidbody2D>().velocity;
            sunRb.velocity = myVelocity/2;
            sunRb.gravityScale = 1;
            sun.GetComponent<TrailRenderer>().enabled = false;
            sunRb.AddTorque(0.08f * myVelocity.magnitude, ForceMode2D.Impulse);
        }
    
        Destroy(myParent);
    }

}
