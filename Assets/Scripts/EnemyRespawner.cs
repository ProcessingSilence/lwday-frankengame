using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private GameObject currentEnemy;
    private bool allowedToRespawn = true;
    // Start is called before the first frame update
    void Start()
    {
        currentEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentEnemy && allowedToRespawn)
        {
            allowedToRespawn = false;
            StartCoroutine(WaitBeforeRespawn());
        }
    }

    IEnumerator WaitBeforeRespawn()
    {
        yield return new WaitForSeconds(1f);
        currentEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        allowedToRespawn = true;
    }
}
