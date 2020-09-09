using UnityEngine;

public class physicsObj : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
}
