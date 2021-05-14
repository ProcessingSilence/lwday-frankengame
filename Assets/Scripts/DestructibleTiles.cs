using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructibleTiles : MonoBehaviour
{
    public ContactPoint2D[] contacts = new ContactPoint2D[10];

    public Transform followObj;

    public GameObject soundPlayer;

    private CircleCollider2D circleCollider2D;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void FixedUpdate()
    {
        transform.position = followObj.position;
        var hit = Physics2D.CircleCastAll(transform.position, circleCollider2D.radius, transform.forward);
        if (hit.Length > 0)
        {
            foreach (var obj in hit)
            {
                if (obj.collider.gameObject.CompareTag("Destroyable"))
                {
                    Debug.Log("hit");
                    NewDestroyTiles(obj);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = followObj.position;
    }

    private void LateUpdate()
    {
        transform.position = followObj.position;
    }



    void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Destroyable")) 
        {
            DestroyTiles(collision);    
        }   
    }


    void DestroyTiles(Collision2D collision)
    {
        Vector3 hitPos = new Vector3();
        var destroyableTilemap = collision.gameObject.GetComponent<Tilemap>();
        foreach (ContactPoint2D hit in collision.contacts)
        {
            hitPos.x = hit.point.x - 0.01f * hit.normal.x;
            hitPos.y = hit.point.y - 0.01f * hit.normal.y;
            destroyableTilemap.SetTile(destroyableTilemap.WorldToCell(hitPos),null);
            
            GameObject newSoundPlayer = Instantiate(soundPlayer);
            AudioSource aS = newSoundPlayer.GetComponent<AudioSource>();
            aS.clip = Resources.Load<AudioClip>("Audio/destroyBlock");
            aS.Play();
        }
    }

    void NewDestroyTiles(RaycastHit2D hit)
    {
        Vector3 hitPos = new Vector3();
        var destroyableTilemap = hit.collider.gameObject.GetComponent<Tilemap>();

        hitPos.x = hit.point.x - 0.01f;
        hitPos.y = hit.point.y - 0.01f;
        destroyableTilemap.SetTile(destroyableTilemap.WorldToCell(hitPos),null);
        
        GameObject newSoundPlayer = Instantiate(soundPlayer);
        AudioSource aS = newSoundPlayer.GetComponent<AudioSource>();
        aS.clip = Resources.Load<AudioClip>("Audio/destroyBlock");
        aS.Play();       
    }
}
