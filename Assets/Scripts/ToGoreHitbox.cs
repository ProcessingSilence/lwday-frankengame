using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Gores enemy upon collision. Uses a smaller collision to detect when to gore so that the enemy doesn't instantly gore
// upon slightly touching the floor by a pixel.

public class ToGoreHitbox : MonoBehaviour
{
    private int beginDeathSequence;
    public GameObject myParent;

    public GameObject goreExplosion;
    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.parent.gameObject;
    }

    // Call death in LateUpdate so if it collides with a button + wall at the same time, it will detect button first.
    void Update()
    {
        DeathSequence();
    }

    void LateUpdate()
    {
        if (beginDeathSequence == 2)
        {
            Destroy(myParent);  
        }
    }

    void DeathSequence()
    {
        if (beginDeathSequence == 1)
        {
            beginDeathSequence = 2;
            Instantiate(goreExplosion, myParent.transform.position, quaternion.identity);         
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8 && beginDeathSequence < 1)
        {
            Debug.Log("Hit Wall");
            beginDeathSequence = 1;
        }
    }
}
