using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAtMouse : MonoBehaviour
{
    public Vector2 mousePos, myPos;
    
    public float angle;
    
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
        myPos = Camera.main.WorldToViewportPoint(transform.position);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = (float) Math.Atan2(myPos.y - mousePos.y, myPos.x - mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180));
    }

    private void GraphicProperties()
    {
        if (angle > 90 || angle < -90)
        {
            face.flipY = false;
        }
        else if (angle < 90 && angle > -90)
        {
            face.flipY = true;
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
