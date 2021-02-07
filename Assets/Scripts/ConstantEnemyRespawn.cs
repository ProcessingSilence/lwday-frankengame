using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantEnemyRespawn : MonoBehaviour
{
    public GameObject enemyToSpawn;
    private GameObject currentEnemy;

    public float waitTime;
    public Vector3 enemyScale;

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
        enemyScale = currentEnemy.transform.localScale;
        currentEnemy.transform.localScale = Vector3.zero;

        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSecondsRealtime(0.01f);
            currentEnemy.transform.localScale += enemyScale / 20;
        }
        
        yield return new WaitForSecondsRealtime(waitTime);
        StartCoroutine(Spawn());
    }
}
