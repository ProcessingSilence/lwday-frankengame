using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectOverlap : MonoBehaviour
{
    public bool isOverlapping;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isOverlapping = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isOverlapping = false;
        }
    }
}
