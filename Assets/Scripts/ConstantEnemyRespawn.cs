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
        StartCoroutine(WaitBeforeRespawn());
    }

    IEnumerator WaitBeforeRespawn()
    {
        currentEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);   
        yield return new WaitForSecondsRealtime(waitTime);
        StartCoroutine(WaitBeforeRespawn());
    }
}
