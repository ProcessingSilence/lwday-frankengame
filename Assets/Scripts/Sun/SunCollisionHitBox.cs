using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCollisionHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HurtPlayer(other.gameObject);
        }
    }

    void HurtPlayer(GameObject player)
    {
        PlayerState playerState = player.GetComponent<PlayerState>();
        if (playerState.currentState != PlayerState.States.Hurt)
        {
            playerState.currentState = PlayerState.States.Hurt;
        }
    }
}
