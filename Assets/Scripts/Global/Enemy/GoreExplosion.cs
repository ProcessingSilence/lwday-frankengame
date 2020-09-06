using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreExplosion : MonoBehaviour
{
    public Sprite[] goryParts;

    public GameObject goreObj;
    private GameObject currentGoreObj;
    
    private AudioSource audioSource;

    public AudioClip[] goreSound;

    private bool spawnOnce;

    private float randomRotation;

    public int goreAmount;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnOnce == false)
        {
            spawnOnce = true;
            for (int i = 0; i < goreAmount; i++)
            {
                randomRotation = Random.Range(0, 360);
                currentGoreObj = Instantiate(goreObj, transform.position, Quaternion.identity);
                currentGoreObj.GetComponent<SpriteRenderer>().sprite = goryParts[Random.Range(0, goryParts.Length)];
                var tempRB = currentGoreObj.GetComponent<Rigidbody2D>();
                currentGoreObj.transform.eulerAngles = new Vector3(0,0,randomRotation);
                tempRB.AddRelativeForce(Random.onUnitSphere * 500);
            }
            audioSource.clip = goreSound[Random.Range(0, goreSound.Length)];
            audioSource.Play();
        }       
    }  
}
