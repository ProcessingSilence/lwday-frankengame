using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Gores enemy upon collision. Uses a smaller collision to detect when to gore so that the enemy doesn't instantly gore
// upon slightly touching the floor by a pixel.

public class ToGoreHitbox : MonoBehaviour
{
    public int beginDeathSequence;
    public GameObject myParent;
    private SpriteRenderer myParentSpriteRenderer;
    public GameObject goreExplosion;
    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.parent.gameObject;
        myParentSpriteRenderer = myParent.GetComponent<SpriteRenderer>();
    }

    // Call death in LateUpdate so if it collides with a button + wall at the same time, it will detect button first.
    void Update()
    {

    }

    void LateUpdate()
    {
        if (beginDeathSequence == 1)
        {
            beginDeathSequence = 2;
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
        if ((other.gameObject.layer == 8 || other.gameObject.layer == 18) && beginDeathSequence < 1)
        {
            Debug.Log("Hit Wall");
            beginDeathSequence = 1;
        }
    }
}
