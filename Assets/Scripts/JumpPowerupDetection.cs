using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerupDetection : MonoBehaviour
{
    private PlayerController PlayerController_script;

    private GameObject powerupObj;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerController_script = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Layers of JumpTrigger and JumpTriggerDetector only detect each other and nothing else.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IncreaseJump"))
        {
            powerupObj = other.gameObject;
            AddToJumpLimit();
        }
    }

    void AddToJumpLimit()
    {
        powerupObj.SetActive(false);
        PlayerController_script.multiJumpLimit += 1;
        Debug.Log("multiJumpLimit increased to " + PlayerController_script.multiJumpLimit);
    }
}
