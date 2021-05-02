using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : StateMachineBehaviour
{
    private Vector2 thrownDireciton;

    private float velocity;
    public GameObject projectileHitbox;

    private Rigidbody2D rb;
    
    private ThrownVals ThrownVals_script;

    public Sprite sunThrownSprite;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animator.transform.name == "Evil Sun")
        {
            SoundSpawner.PlaySoundObj(animator.transform.position, Resources.Load<AudioClip>("Audio/sunThrown"), 1);
            animator.GetComponent<SpriteRenderer>().sprite = sunThrownSprite;
        }
        ThrownVals_script = animator.gameObject.GetComponent<ThrownVals>();
        animator.gameObject.layer = 15;
        projectileHitbox = Instantiate(projectileHitbox,animator.transform.position, Quaternion.identity);
        projectileHitbox.transform.parent = animator.gameObject.transform;
        if (ThrownVals_script.regularExplosion)
        {
            projectileHitbox.transform.GetChild(0).GetComponent<ToGoreHitbox>().goreExplosion =
                Resources.Load<GameObject>("Explosion");
        }
        // Instantly kill if the spawn position of the thrown projectile overlaps a layer.

        rb = animator.gameObject.GetComponent<Rigidbody2D>();
        velocity = animator.gameObject.GetComponent<ThrownVals>().givenVelocity;



        ThrownVals_script.WaitUntilExploding();

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // Prevent player from throwing enemy through the tilemap.
        if (ThrownVals_script.instaKill)
        {
            projectileHitbox.transform.GetChild(0).GetComponent<ToGoreHitbox>().beginDeathSequence = true;
            Debug.Log("NO BULLET-THROUGH-PAPER FOR YOU, PROJECTILE! INSTA-KILLED.");
        }
        
        if (ThrownVals_script.instaKill == false)
            rb.velocity = (animator.transform.right * (velocity * 200) * Time.deltaTime);
    }
    
}
