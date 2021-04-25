using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandHitbox : MonoBehaviour
{
    [SerializeField]private BoxCollider2D collider;

    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Land") && collider.enabled)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
            collider.enabled = false;
        }
    }
}
