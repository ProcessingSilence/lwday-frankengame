﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject wall;
    public GameObject[] appearObj;
    public GameObject[] walls;
    private Button Button_script;
    public Sprite[] buttonStates;
    private SpriteRenderer spriteRenderer;
    public int buttonHit;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Button_script = GetComponent<Button>();

        for (int i = 0; i < appearObj.Length; i++)
        {
            if (appearObj[i])
            {
                appearObj[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHit == 1)
        {
            buttonHit = 2;
            if (walls.Length > 0)
            {
                foreach (GameObject item in walls)
                {
                    Destroy(item);
                }
            }

            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = Resources.Load<AudioClip>("click");
            _audioSource.Play();
            if (wall)
            {
                Destroy(wall);
            }

            if (spriteRenderer.enabled)
            {
                spriteRenderer.sprite = buttonStates[1];
            }
            if (appearObj.Length > 0)
            {
                for (int i = 0; i < appearObj.Length; i++)
                {
                    if (appearObj[i])
                    {
                        appearObj[i].SetActive(true);
                    }
                }
            }

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
