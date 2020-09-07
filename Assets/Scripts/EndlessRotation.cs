using UnityEngine;

public class EndlessRotation : MonoBehaviour
{
    public float rotateSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,rotateSpeed * Time.deltaTime);
    }
}
