using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private int hitGoal;

    private GameObject mainSceneManager;

    private MainSceneManager MainSceneManager_script;
    // Start is called before the first frame update
    void Start()
    {
        mainSceneManager = GameObject.FindWithTag("SceneManager");
        MainSceneManager_script = mainSceneManager.GetComponent<MainSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitGoal == 1)
        {
            hitGoal = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitGoal = 1;
        }
    }
}
