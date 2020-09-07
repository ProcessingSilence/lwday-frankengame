using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

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
    private int beginDeathSequence;
    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.gameObject;
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

    // Update is called once per frame
    void Update()
    {
        PressButton();
        DeathSequence();
        KillOtherEnemy();
    }

    void PressButton()
    {
        if (buttonObj && alreadyPressedButton == false)
        {
            alreadyPressedButton = true;
            var Button_script = buttonObj.GetComponent<Button>();
            Button_script.buttonHit = 1;

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

    void DeathSequence()
    {
        if (beginDeathSequence == 1)
        {
            beginDeathSequence = 2;
            Instantiate(goreExplosion, transform.position, quaternion.identity);
            Destroy(myParent);           
            gameObject.GetComponent<ProjectileHitbox>().enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Begin death sequence upon detecting surface.
        if (other.gameObject.layer == 8 && beginDeathSequence < 1)
        {
            beginDeathSequence = 1;
        }

        if (other.CompareTag("Enemy") && otherEnemy == false)
        {
            otherEnemy = other.gameObject;
        }

        if (other.gameObject.layer == 16)
        {
            buttonObj = other.gameObject;
        }
    }
}
