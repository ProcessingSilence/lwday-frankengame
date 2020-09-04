using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    private int activateMoreJump;

    private PlayerController PlayerController_script;

    // Update is called once per frame
    void Update()
    {
        if (activateMoreJump == 1)
        {
            activateMoreJump = 2;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && activateMoreJump < 1)
        {
            activateMoreJump = 1;
            PlayerController_script = other.GetComponent<PlayerController>();
        }
    }
}
