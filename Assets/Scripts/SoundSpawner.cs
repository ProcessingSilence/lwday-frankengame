using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSpawner : MonoBehaviour
{
    public GameObject soundPlay;

    public static GameObject soundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        soundPlayer = soundPlay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySoundObj(Vector3 position, AudioClip clip, float volume = 1, bool hasLimitedRange = false, float maxDist = 80)
    {
        var currentSoundPlayer = Instantiate(soundPlayer, position, Quaternion.identity).GetComponent<AudioSource>();
        if (hasLimitedRange)
        {
            currentSoundPlayer.rolloffMode = AudioRolloffMode.Linear;
            currentSoundPlayer.maxDistance = maxDist;
        }

        currentSoundPlayer.volume = volume;
        currentSoundPlayer.clip = clip;       
    }
}
