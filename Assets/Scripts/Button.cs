﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject wall;
    private Button Button_script;
    public Sprite[] buttonStates;
    private SpriteRenderer spriteRenderer;
    public int buttonHit;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Button_script = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHit == 1)
        {
            buttonHit = 2;
            Destroy(wall);
            spriteRenderer.sprite = buttonStates[1];
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(Button_script);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            buttonHit = 1;
        }
    }
}
