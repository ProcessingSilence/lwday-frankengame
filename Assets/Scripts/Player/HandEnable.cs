using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnable : MonoBehaviour
{
    public GameObject hand;
    private GameObject currentThrownHand;
    public GameObject caughtEnemy;
    public GameObject aimingObj;    
    
    private bool wasThrownFlag;

    private int throwDirection;

    private HandThrow HandThrow_script;
    private HandEnable HandEnable_script;
    private AimingAtMouse AimingAtMouse_script; 
    public PlayerController PlayerController_script;


    // Start is called before the first frame update
    void OnEnable()
    {
        HandEnable_script = gameObject.GetComponent<HandEnable>();
        hand.SetActive(false);
        AimingAtMouse_script = aimingObj.GetComponent<AimingAtMouse>();
        aimingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.activeSelf == false && caughtEnemy == false)
        {
            wasThrownFlag = false;
        }

        EnableOnMouseDown();
        if (caughtEnemy)
        {
            aimingObj.SetActive(true);
            AimingAtMouse_script.enemyObj = caughtEnemy;
            HandThrow_script.enabled = false;
        }
    }

    private void EnableOnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (wasThrownFlag == false && caughtEnemy == false)
            {
                wasThrownFlag = true;
                hand.SetActive(true);
            }
        }
    }
}
