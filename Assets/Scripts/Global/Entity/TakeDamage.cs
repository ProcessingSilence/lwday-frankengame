using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public bool takeEnemyDamage;
    public GameObject goreSpawn;
    private GameObject currentGoreSpawn;
    private int dieOnce;
    // Start is called before the first frame update

    // Update is called once per frame
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
            currentGoreSpawn = Instantiate(goreSpawn, transform.position, quaternion.identity);
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
        if (other.gameObject.CompareTag("Damage"))
        {
            if (dieOnce == 0)
                dieOnce = 1;
        }

        if (other.gameObject.CompareTag("DamageEnemy") && takeEnemyDamage)
        {
            if (dieOnce == 0)
                dieOnce = 1;
        }
    }
}
