using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSpiker : MonoBehaviour
{
    public GameObject playerObj;

    public TakeDamage takeDamage;

    public CircleCollider2D circleCollider;

    public float requiredDist;

    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        playerObj = GameObject.Find("Player");
        takeDamage = playerObj.GetComponent<PlayerState>().takeDamage;
        requiredDist = (circleCollider.radius * 2) + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerObj.transform.position);
        if (distance < requiredDist)
        {
            Debug.Log("REQUIRED DISTANCE");
            InstakillPlayer();
        }
    }

    void InstakillPlayer()
    {
        if (takeDamage.health > 0)
        {
            takeDamage.dieOnce = 1;
        }
    }
}
