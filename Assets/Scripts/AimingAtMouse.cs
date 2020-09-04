using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAtMouse : MonoBehaviour
{
    public Vector2 mousePos, myPos;
    public float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myPos = Camera.main.WorldToViewportPoint(transform.position);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = (float) Math.Atan2(myPos.y - mousePos.y, myPos.x - mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
