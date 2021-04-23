using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingEffect : MonoBehaviour
{
    public Transform cameraObj;

    public float speed;

    private Vector2 startingTrans;
    // Start is called before the first frame update
    void Awake()
    {
        cameraObj = GameObject.Find("Main Camera").transform;
        startingTrans = transform.position;
    }
 

    void LateUpdate()
    {
        transform.position = startingTrans + new Vector2(-cameraObj.transform.position.x * speed, -cameraObj.transform.position.y * speed);
    }
}