using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private int hitGoal;

    private GameObject mainSceneManager;

    private MainSceneManager MainSceneManager_script;

    private AudioSource audioSource;

    private ParticleSystem particleSystem;

    private SpriteRenderer spriteRenderer;

    private CircleCollider2D circleCollider2D;

    public bool isFakeGoal;
    // Start is called before the first frame update
    void Start()
    {
        mainSceneManager = GameObject.FindWithTag("SceneManager");
        if (isFakeGoal == false)
        {
            MainSceneManager_script = mainSceneManager.GetComponent<MainSceneManager>();
        }
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitGoal == 1)
        {
            hitGoal = 2;
            circleCollider2D.enabled = false;
            if (audioSource.clip)
            {
                audioSource.Play();
                particleSystem.Play();
            }
            spriteRenderer.enabled = false;
            if (isFakeGoal == false)
            {
                MainSceneManager_script.sceneNum = 2;
            }

            if (isFakeGoal)
            {
                SoundSpawner.PlaySoundObj(transform.position, audioSource.clip);
                spriteRenderer.sprite = null;
            }

            StartCoroutine(WaitBeforeDestroy());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hitGoal != 1) 
        {
            hitGoal = 1;
        }
    }

    IEnumerator WaitBeforeDestroy()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
    }
}
