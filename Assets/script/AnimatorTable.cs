using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTable : MonoBehaviour {
    public Animator animator;
    private RuntimeAnimatorController origin=null;
    public sbyte controtionNo;
    public float controtTimeLeft = 0;
    // Use this for initialization
    void Start () {
        
        animator = GetComponent<Animator>();
      

    }
	public RuntimeAnimatorController controler {
        set
        {
            Debug.Log("进入controler origin is "+origin);
            if (origin == null)
            {
                Debug.Log("进入origin");
                origin = animator.runtimeAnimatorController;
                animator.runtimeAnimatorController = value;
            }
        }
        get
        {
            return animator.runtimeAnimatorController;
        }
    }

	// Update is called once per frame
    public void restoreAnimator()
    {
        if (origin != null)
        {
            animator.runtimeAnimatorController = origin;
            origin = null;
        }
    }
	void Update () {
		
	}
    public void moveStart()
    {
        animator.SetBool("move", true);
    }
    public void moveEnd()
    {
        animator.SetBool("move", false);
    }
    public void AttackStart()
    {
        animator.SetBool("attack", true);
    }
    public void AttackEnd()
    {
        animator.SetBool("attack", false);
    }
    public void SkillStart()
    {
        animator.SetBool("skill", true);
    }
    public void SkillEnd()
    {
        animator.SetBool("skill", false);
    }
    public void StiffStart()
    {
        animator.SetBool("stiff",true);
    }
    public void StiffEnd()
    {
        animator.SetBool("stiff", false);
    }
    public void ConverselyStart()
    {
        animator.SetBool("conversely", true);
    }
    public void ConverselyEnd()
    {
        animator.SetBool("conversely", false);
    }

}