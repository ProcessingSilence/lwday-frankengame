using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoalUponDeaths : MonoBehaviour
{
    public Transform[] enemiesToKill;
    private bool confirmGoal;
    //public string FindObjTag;
    public GameObject GoalObj;
    private AudioSource audioSource;

    private ParticleSystem particleSystem;

    public bool getAllEnemies;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        GoalObj.SetActive(false);
        if (getAllEnemies)
        {
            GameObject[] tempEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmpty(enemiesToKill) && confirmGoal == false)
        {
            confirmGoal = true;
            StartCoroutine(SpawnGoal());
        }
    }
    
    private bool isEmpty(Transform[] arr)
    {
        for(int i = 0; i < arr.Length; i++) 
        {
            if (arr[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnGoal()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        transform.position = GoalObj.transform.position;
        GoalObj.SetActive(true);
        audioSource.Play();
        particleSystem.Play();
    }
}
