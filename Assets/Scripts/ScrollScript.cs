using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{
    public float speed = 0;

    public Renderer myRenderer;

    public Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
        if (!playerPos)
        {
            Debug.Log("Playerpos not given, turning off scroll script");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        myRenderer.material.mainTextureOffset = playerPos.position * speed;
    }
}
