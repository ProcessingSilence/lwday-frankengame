using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtCheck : MonoBehaviour
{
    public bool isCaught;
    public Transform hand;
    
    void Update()
    {
        if (isCaught)
        {
            transform.position = hand.position;
            transform.rotation = hand.rotation;
        }
    }
}
