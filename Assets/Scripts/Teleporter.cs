﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Transform exit;

    private Transform player;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        var index = transform.GetSiblingIndex();
        exit = transform.parent.GetChild(index + 1).gameObject.transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            OnTeleport();
        }
    }

    void OnTeleport()
    {
        player.transform.position = exit.position;
        audioSource.Play();
    }
}