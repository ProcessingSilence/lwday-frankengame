using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Transform exit;

    private Transform teleportObj;

    private AudioSource audioSource;

    public ParticleSystem[] entranceExitEffects;
    // Start is called before the first frame update
    void Awake()
    {
        var index = transform.GetSiblingIndex();
        exit = transform.parent.GetChild(index + 1).gameObject.transform;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy") || other.CompareTag("DamageEnemy") || other.CompareTag("Projectile") || other.CompareTag("Damage") || other.CompareTag("SpikeyEnemy"))
        {
            teleportObj = other.transform;
            OnTeleport();
        }
    }

    void OnTeleport()
    {
        entranceExitEffects[0].gameObject.transform.position = teleportObj.transform.position;
        teleportObj.transform.position = exit.position;
        audioSource.Play();

        entranceExitEffects[0].Play();
        entranceExitEffects[1].Play();
    }
}
