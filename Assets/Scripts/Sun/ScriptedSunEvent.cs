using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptedSunEvent : MonoBehaviour
{
    public Sprite eyesOpen;

    public Sprite bite;

    public Sprite evilFace;
    public Sprite laugh;

    public SpriteRenderer sunSprite;
    public GameObject sun;
    public Transform falseMeat;

    public Transform player;

    public bool beginAnimation;

    public AudioSource audioSource;

    public AudioClip roar;
    public AudioClip chomp;

    private Coroutine animation;

    private bool moveToMeat;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        //SoundSpawner.PlaySoundObj(transform.position, roar);
    }

    private void FixedUpdate()
    {
        if (moveToMeat)
        {
            var step =  speed * Time.deltaTime;
            sun.transform.position = Vector3.MoveTowards(sun.transform.position, falseMeat.position, step);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(player.position, falseMeat.position);
        if (distance <= 2.1f)
        {
            if (animation == null)
            {
                animation = StartCoroutine(SunAnimation());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && animation == null)
        {
            sunSprite.sprite = eyesOpen;
        }
    }

    IEnumerator SunAnimation()
    {
        SoundSpawner.PlaySoundObj(transform.position, roar);
        SoundSpawner.PlaySoundObj(transform.position, roar);
        sunSprite.color = new Color(sunSprite.color.r, sunSprite.color.g, sunSprite.color.b, 1);
        sun.GetComponent<ScrollingEffect>().enabled = false;
        sunSprite.sprite = bite;
        yield return new WaitForSecondsRealtime(0.1f);
        moveToMeat = true;
        yield return new WaitForSecondsRealtime(0.1f);
        sunSprite.sprite = evilFace;
        SoundSpawner.PlaySoundObj(transform.position, chomp);
        falseMeat.GetComponent<SpriteRenderer>().enabled = false;
        falseMeat.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSecondsRealtime(1f);
        sunSprite.sprite = laugh;
        yield return new WaitForSecondsRealtime(0.3f);
        sunSprite.sprite = evilFace;
        yield return new WaitForSecondsRealtime(0.3f);
        sunSprite.sprite = laugh;
        yield return new WaitForSecondsRealtime(0.3f);
        sunSprite.sprite = evilFace;
        yield return new WaitForSecondsRealtime(0.3f);
        sunSprite.sprite = laugh;
        yield return new WaitForSecondsRealtime(0.8f);
        sunSprite.sprite = evilFace;
        SceneManager.LoadScene("Level_15");

    }
}
