using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHand : MonoBehaviour
{
    private int hitEnemy;

    private GameObject caughtEnemy;

    public float countdown;

    private CaughtCheck CaughtCheck_script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (hitEnemy == 1)
        {
            hitEnemy = 2;
            CaughtCheck_script.hand = gameObject.transform;
            CaughtCheck_script.isCaught = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            hitEnemy = 1;
            caughtEnemy = other.gameObject;
        }
    }
}
