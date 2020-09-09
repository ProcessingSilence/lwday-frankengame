using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    private float deathPos;

    public bool isImportant;

    public GameObject goreObj;
    // Start is called before the first frame update
    void Start()
    {
        deathPos = GlobalVars.vars.deathPos;
        for (int i = 0; i < GlobalVars.vars.importantTags.Length - 1; i++)
        {
            if (gameObject.CompareTag(GlobalVars.vars.importantTags[i]))
            {
                isImportant = true;
            }
        }     
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= deathPos)
        {
            if (isImportant)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
