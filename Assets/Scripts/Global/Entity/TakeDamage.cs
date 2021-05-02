using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public GameObject goreSpawn;
    private GameObject currentGoreSpawn;
    [HideInInspector]public int dieOnce;

    public GameObject healthObj;
    private GameObject newHealthObj;
    public GameObject healthBar;

    public Sprite barOn, barOff;

    public int health;

    public Coroutine damageState;

    public GameObject playerObj;
    private PlayerState playerState;
    void Awake()
    {
        playerState = playerObj.GetComponent<PlayerState>();
        damageState = null;
        Transform canvasObj = GameObject.Find("Canvas").transform;
        newHealthObj = Instantiate(healthObj, canvasObj.transform, false);
        for (int i = 0; i < health; i++)
        {
            GameObject tempBar = Instantiate(healthBar, newHealthObj.transform, true);
        }

        transform.parent = null;
    }

    private void LateUpdate()
    {
        if (playerObj)
        {
            transform.position = playerObj.transform.position;
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
            health = 0;
            foreach (Transform child in newHealthObj.transform)
            {
                child.GetComponent<Image>().sprite = barOff;
            }
            Destroy(gameObject);
        }
    }

    void DestroyObj()
    {
        if (currentGoreSpawn && playerObj.activeSelf)
        {
            playerObj.SetActive(false);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (playerState.currentState != PlayerState.States.Hurt ||
            playerState.currentState != PlayerState.States.HurtInvulnerable)
        {
            //Debug.Log("go");
            if (other.gameObject.CompareTag("Damage") || other.gameObject.CompareTag("DamageEnemy") || other.gameObject.CompareTag("SpikeyEnemy"))
            {
                dieOnce = 1;
            }

            if (((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("UnthrowableEnemy")) || other.gameObject.CompareTag("SunHurtPlayer") || other.gameObject.CompareTag("Missile") || other.gameObject.layer == 18)  && damageState == null)
            {
                if (other.name != "Evil Sun")
                {
                    damageState = StartCoroutine(TouchEnemy(other));
                }
            }
        }
    }


    private IEnumerator TouchEnemy(Collider2D other)
    {
        playerState.currentState = PlayerState.States.Hurt;
        health--;
        newHealthObj.transform.GetChild(health).GetComponent<Image>().sprite = barOff;
        if (health >= 1)
        {
            if (other.transform.transform.position.x > playerObj.transform.position.x)
            {
                playerState.leftOrRight = false;
            }
            else
            {
                playerState.leftOrRight = true;
            }
        }

        else if (dieOnce == 0 && health < 1)
        {
            dieOnce = 1;
        }        
        yield return new WaitForSecondsRealtime(0.001f);
    }
}
