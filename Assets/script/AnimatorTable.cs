using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTable : MonoBehaviour {
    public Animator animator;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
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
   
}
