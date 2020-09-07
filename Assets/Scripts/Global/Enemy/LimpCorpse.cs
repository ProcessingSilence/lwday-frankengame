using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpCorpse : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Color deathColor = new Color(128,128,128);
    private LimpCorpse LimpCorpse_script;
    private ThrownVals ThrownVals_script;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        ThrownVals_script = animator.gameObject.GetComponent<ThrownVals>();
    }

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
            spriteRenderer.sprite = ThrownVals_script.deathSprite;
            LimpCorpse_script = animator.gameObject.GetComponent<LimpCorpse>();
        }
        else if (spriteRenderer.color.r > deathColor.r)
        {
            spriteRenderer.color -= new Color(1,1,1);
        }
        else
        {
            animator.SetBool("dead", false);
            animator.SetBool("exitAnimation", true);
        }
    }
}
