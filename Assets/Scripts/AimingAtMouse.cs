using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingAtMouse : MonoBehaviour
{
    public Vector2 mousePos, myPos;
    public float angle;
    public SpriteRenderer childSpriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        childSpriteRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myPos = Camera.main.WorldToViewportPoint(transform.position);
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        angle = (float) Math.Atan2(myPos.y - mousePos.y, myPos.x - mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 180));
    }

    private void LateUpdate()
    {
        if (angle > 90 || angle < -90)
        {
            childSpriteRenderer.flipY = false;
        }
        else if (angle < 90 && angle > -90)
        {
            childSpriteRenderer.flipY = true;
        }
    }
}
