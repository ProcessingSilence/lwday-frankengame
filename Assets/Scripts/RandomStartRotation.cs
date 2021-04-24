using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomStartRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = quaternion.Euler(0,0,Random.Range(-360, 360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
