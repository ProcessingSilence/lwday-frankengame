using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEnable : MonoBehaviour
{
    public GameObject hand;
    private GameObject currentThrownHand;
    private bool wasThrownFlag;
    public GameObject caughtEnemy;
    private int throwDirection;

    private HandThrow HandThrow_script;
    
    public PlayerController PlayerController_script;
    private AimingAtMouse AimingAtMouse_script; 
    public GameObject aimingObj;
    // Start is called before the first frame update
    void OnEnable()
    {
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
