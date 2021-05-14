using System.Collections;
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
    private GameObject regularExplosion;
    private Rigidbody2D rb2d;
    private float angle;

    private Vector3 endPoint;
    private bool endPointDetected;
    public LayerMask landLayer;
    private Rigidbody2D rb;
    public float givenVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (!goreExplosion)
        {
            goreExplosion = Resources.Load("GoreSpawner") as GameObject;
        }
        myParent = gameObject.transform.parent.parent.gameObject;
        rb = myParent.GetComponent<Rigidbody2D>();
        givenVelocity = myParent.GetComponent<ThrownVals>().givenVelocity;
        //myParent.GetComponent<BoxCollider2D>().enabled = false;
        myParentSpriteRenderer = myParent.GetComponent<SpriteRenderer>();
    }

    /*
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, myParent.transform.rotation * Vector2.right, 100);
        Debug.DrawRay(transform.position, myParent.transform.rotation * Vector2.right * 100f, Color.green);

        if (!hit.collider.gameObject.CompareTag("ProjectileHitbox") && hit.collider.gameObject != gameObject && !hit.collider.gameObject.CompareTag("PlayerComponent"))
        {
            Debug.Log("HIT OBJ: " + hit.collider.gameObject.name);
            beginDeathSequence = true;      
        }
        
    }
    */

    private void FixedUpdate()
    {
        if (beginDeathSequence == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, myParent.transform.right,Mathf.Infinity, landLayer);
            if (hit.collider)
            {
                endPoint = hit.point;
                endPointDetected = true;
                Vector3 hitPoint3D = new Vector3(hit.point.x,hit.point.y, 0);
                Vector3 toTarget = (hitPoint3D - transform.position).normalized;
                if (!(Vector2.Dot(toTarget, myParent.transform.right) > 0))
                {
                    Debug.Log("In the raycast zone");
                    beginDeathSequence = true;
                }
            }
            if (!hit.collider)
            {
                endPointDetected = false;
                Debug.Log("No hit point detected.");
            }
        }

        if (beginDeathSequence == false)
        {
            rb.velocity = (myParent.transform.right * (givenVelocity * 200) * Time.deltaTime);
        }

        if (beginDeathSequence == true)
        {
            rb.velocity = Vector2.zero;
        }



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
        rb.velocity = Vector2.zero;
        myParent.transform.position = endPoint;
        var goreSpawnCheck= Instantiate(goreExplosion, myParent.transform.position, quaternion.identity);
        myParentSpriteRenderer.enabled = false;
        yield return new WaitUntil(() => goreSpawnCheck == true);
            
        Destroy(myParent);                 
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (DetectedObj(other.gameObject))
        {
            beginDeathSequence = true;
        }
    }

    
    private bool DetectedObj(GameObject obj)
    {
        if ((obj.gameObject.layer == 8 || obj.gameObject.layer == 18) && beginDeathSequence == false && !obj.CompareTag("Projectile"))
        {
            if (!obj.CompareTag("Destroyable"))
                return true;
        }

        return false;
    }
   

    bool OppositeSigns(float a, float b) 
    {
        return a*b >= 0.0f;
    }
}
