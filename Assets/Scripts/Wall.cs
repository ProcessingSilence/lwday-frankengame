using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject targetObj;
    // Start is called before the first frame update
    void Awake()
    {
        if (!targetObj)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetObj)
        {
            Destroy(gameObject);
        }
    }
}
