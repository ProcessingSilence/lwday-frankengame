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

    public bool enableThrowGraphics;

    public GameObject enemyObj;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        AimRotation();
        GraphicProperties();
        EnemyPosAndRot();
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
}
