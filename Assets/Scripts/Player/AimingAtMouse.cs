using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAtMouse : MonoBehaviour
{
    public Vector2 mousePos, myPos;
    
    public Vector2 angle;
    
    public SpriteRenderer face;
    
    public Transform handGraphicPos;
 
    public PlayerController PlayerController_script;
    private ThrownVals ThrownVals_script;
    public HandEnable HandEnable_script;
    
    public GameObject enemyObj;

    private Animator enemyAnimator;
    
    private bool haveThrown;
    // Start is called before the first frame update

    void OnEnable()
    {
        PlayerController_script.chosenSprite = 3;
    }

    // Update is called once per frame
    void Update()
    {
        AimRotation();
        GraphicProperties();
        EnemyPosAndRot();
        MouseInput();
        SetThrownProperties();
    }
    
    private void AimRotation()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = new Vector2(mousePos.x - transform.position.x,mousePos.y - transform.position.y);
        transform.right = angle;
    }

    private void GraphicProperties()
    {
        if (transform.eulerAngles.z < 90 && transform.eulerAngles.z > -90)
        {
            face.flipY = false;
            Debug.Log("flip false");
            Debug.Log(transform.rotation.z);
        }
        else
        {
            face.flipY = true;
            Debug.Log("flip true");
        }
    }

    private void EnemyPosAndRot()
    {
        if (enemyObj)
        {
            enemyObj.transform.position = handGraphicPos.position;
            enemyObj.transform.rotation = transform.rotation;
        }
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            haveThrown = true;
        }
    }

    private void SetThrownProperties()
    {
        if (haveThrown)
        {
            ThrownVals_script = enemyObj.GetComponent<ThrownVals>();
            ThrownVals_script.givenVelocity = 70;
            enemyObj.transform.position = face.transform.position;
            enemyObj.GetComponent<Animator>().SetBool("thrown", true);
            enemyObj = null;
            PlayerController_script.chosenSprite = 1;
            haveThrown = false;
            StartCoroutine(StartHandEnableDelay());

        }
    }

    IEnumerator StartHandEnableDelay()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        HandEnable_script.wasThrownFlag = false;
        HandEnable_script.enabled = true;
        gameObject.SetActive(false);
    }
}
