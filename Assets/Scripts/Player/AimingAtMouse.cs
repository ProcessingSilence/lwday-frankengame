using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAtMouse : MonoBehaviour
{
    public Vector2 mousePos, myPos;
    
    public Vector2 angle;
    
    public SpriteRenderer face;
    public Transform throwLocation;
    public Transform handGraphicPos;
 
    public PlayerController PlayerController_script;
    private ThrownVals ThrownVals_script;
    public HandEnable HandEnable_script;
    
    public GameObject enemyObj;

    private Animator enemyAnimator;
    
    private bool haveThrown;

    public DetectOverlap detectOverlap_script;
    // Start is called before the first frame update

    [SerializeField]private bool testThrow;

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
        if ((transform.eulerAngles.z < 90  && transform.eulerAngles.z >= 0) || (transform.eulerAngles.z <= 360 && transform.eulerAngles.z > 270))
        {
            face.flipY = false;
            //Debug.Log("flip false");
            //Debug.Log(transform.eulerAngles.z);
        }
        else
        {
            face.flipY = true;
            //Debug.Log("flip true");
            //Debug.Log(transform.eulerAngles.z);
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
        if (Input.GetMouseButtonDown(0) || testThrow)
        {
            testThrow = false;
            haveThrown = true;
        }
    }

    private void SetThrownProperties()
    {
        if (haveThrown)
        {
            if (transform.eulerAngles.z <= 300 && transform.eulerAngles.z >= 240)
            {
                PlayerController_script.jumpFromThrowingEnemy = true;
            }
            // Instantiate AudioObj from Resources to make "woosh" sound.
            var audioObj = Instantiate(Resources.Load("AudioObj") as GameObject);
            audioObj.GetComponent<AudioObj>().givenAudio = Resources.Load("Audio/throwSound2") as AudioClip;
            
            ThrownVals_script = enemyObj.GetComponent<ThrownVals>();
            ThrownVals_script.givenVelocity = 70;
            enemyObj.transform.position = throwLocation.position;
            enemyObj.GetComponent<BoxCollider2D>().size = new Vector2(0.05f,0.05f);
            if (detectOverlap_script.isOverlapping)
            {
                ThrownVals_script.instaKill = true;
            }

            enemyObj.GetComponent<Animator>().SetBool("thrown", true);
            enemyObj.GetComponent<BoxCollider2D>().enabled = true;
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
