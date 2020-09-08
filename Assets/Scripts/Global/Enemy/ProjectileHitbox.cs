using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Detects when enemies are hit or when buttons are pressed, uses a larger hitbox than ToGoreHitbox so it's more forgiving
// to the player to hit the objects.

public class ProjectileHitbox : MonoBehaviour
{
    private GameObject myParent;
    private GameObject goreExplosion;
    private ThrownVals ThrownVals_script;
    private Animator enemyAnimator;
    private GameObject otherEnemy;
    private GameObject buttonObj;
    private bool alreadyPressedButton;
    private bool goreOrCorpse;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.gameObject;
        ThrownVals_script = myParent.GetComponent<ThrownVals>();
        enemyAnimator = myParent.GetComponent<Animator>();
        goreExplosion = ThrownVals_script.goreMess;
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
            var Button_script = buttonObj.GetComponent<Button>();
            Button_script.buttonHit = 1;
            if (Button_script.buttonHit == 1)
            {
                transform.GetChild(0).GetComponent<ToGoreHitbox>().beginDeathSequence = 1;
            }
        }
    }

    void KillOtherEnemy()
    {
        if (otherEnemy)
        {
            Instantiate(goreExplosion, otherEnemy.transform.position, Quaternion.identity);
            Destroy(otherEnemy);
            otherEnemy = null;
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("DamageEnemy")) && otherEnemy == false)
        {
            otherEnemy = other.gameObject;
        }

        if (other.gameObject.layer == 16)
        {
            buttonObj = other.gameObject;
        }
    }
}
