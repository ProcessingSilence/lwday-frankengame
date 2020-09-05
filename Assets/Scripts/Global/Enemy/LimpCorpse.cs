using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpCorpse : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color deathColor = new Color(128,128,128);
    private LimpCorpse LimpCorpse_script;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // Set Layer to "Corpse"
        if (animator.gameObject.layer != 11)
        {
            animator.gameObject.layer = 11;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 1;
            spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = -1;
            LimpCorpse_script = animator.gameObject.GetComponent<LimpCorpse>();
        }
        else if (spriteRenderer.color.r > deathColor.r)
        {
            spriteRenderer.color -= new Color(1,1,1);
        }
        else
        {
            animator.SetBool("dead", false);
            animator.SetBool("disableAll", true);
        }
    }
}
