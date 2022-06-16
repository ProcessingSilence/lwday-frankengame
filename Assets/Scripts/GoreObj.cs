using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoreObj : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -300)
        {
            Destroy(gameObject);
        }
    }
}
