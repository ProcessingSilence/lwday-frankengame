﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Gores enemy upon collision. Uses a smaller collision to detect when to gore so that the enemy doesn't instantly gore
// upon slightly touching the floor by a pixel.

public class ToGoreHitbox : MonoBehaviour
{
    public bool beginDeathSequence;
    public GameObject myParent;
    private SpriteRenderer myParentSpriteRenderer;
    public GameObject goreExplosion;

    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.parent.gameObject;
        myParent.GetComponent<BoxCollider2D>().enabled = false;
        myParentSpriteRenderer = myParent.GetComponent<SpriteRenderer>();
    }

    // Call death in LateUpdate so if it collides with a button + wall at the same time, it will detect button first.
    void Update()
    {
        if (beginDeathSequence)
        {
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        var goreSpawnCheck= Instantiate(goreExplosion, myParent.transform.position, quaternion.identity);
        myParentSpriteRenderer.enabled = false;
        yield return new WaitUntil(() => goreSpawnCheck == true);
            
        Destroy(myParent);                 
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        beginDeathSequence = DetectedObj(other.gameObject);
    }

    private bool DetectedObj(GameObject obj)
    {
        if ((obj.gameObject.layer == 8 || obj.gameObject.layer == 18) && beginDeathSequence == false)
        {
            Debug.Log("Hit Wall");
            return true;
        }

        return false;
    }
}
