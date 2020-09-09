using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantEnemyRespawn : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private GameObject currentEnemy;

    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitBeforeSpawning());
    }

    IEnumerator WaitBeforeSpawning()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        currentEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);   
        yield return new WaitForSecondsRealtime(waitTime);
        StartCoroutine(Spawn());
    }
}
