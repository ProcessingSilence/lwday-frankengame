using UnityEngine;

public class AudioObj : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip givenAudio;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = givenAudio;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
