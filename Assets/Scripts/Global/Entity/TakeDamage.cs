using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public GameObject goreSpawn;
    private GameObject currentGoreSpawn;
    private int dieOnce;

    public GameObject healthObj;
    private GameObject newHealthObj;
    public GameObject healthBar;

    public Sprite barOn, barOff;

    public int health;

    [HideInInspector]public PlayerState playerState;
    void Awake()
    {
        Transform canvasObj = GameObject.Find("Canvas").transform;
        newHealthObj = Instantiate(healthObj);
        newHealthObj.transform.SetParent(canvasObj.transform, false);
        for (int i = 0; i < health; i++)
        {
            GameObject tempBar = Instantiate(healthBar);
            tempBar.transform.parent = newHealthObj.transform;            
        }
    }


    void Update()
    {
        SpawnGore();
        DestroyObj();
    }


    void SpawnGore()
    {
        if (dieOnce == 1)
        {
            dieOnce = 2;
            currentGoreSpawn = Instantiate(goreSpawn, transform.position, Quaternion.identity);
        }
    }

    void DestroyObj()
    {
        if (currentGoreSpawn && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("DamageEnemy"))
        {
            dieOnce = 1;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            newHealthObj.transform.GetChild(health).GetComponent<Image>().sprite = barOff;
            if (health >= 1)
            {
                playerState.currentState = PlayerState.States.Hurt;
                if (other.transform.transform.position.x > gameObject.transform.position.x)
                {
                    playerState.leftOrRight = false;
                }
                else
                {
                    playerState.leftOrRight = true;
                }
            }

            else if (dieOnce == 0 && health < 1)
                dieOnce = 1;
        }
    }
}
