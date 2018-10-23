using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_Anim : MonoBehaviour {
    public const int ATTACK = 2;
    public const int WALK = 1;
    public const int NORMAL = 0;
    public Animator lefthand;
    public Animator righthand;
    public Animator anim;
    public Anim_Mobile baseAnim;
    private int lastMove = 0;

    // Use this for initialization
    void Start () {
        lefthand = transform.Find("左手").GetComponent<Animator>();	
        righthand =  transform.Find("右手").GetComponent<Animator>();
        baseAnim = transform.parent.GetComponent<Anim_Mobile>();
        anim = GetComponent<Animator>();
    }

    public void setHand(int no) {
        Debug.Log("設置手被呼叫:" + no);
        if (no < 3)
        {
            lefthand.SetInteger("handNo", no);
        }
        else {
            righthand.SetInteger("handNo", no - 3); 
        }
    }
    public void setAction(int no)
    {
        lastMove = anim.GetInteger("action");
        anim.SetInteger("action", no);
        
    }
    public void onAttackEnd()
    {
        anim.SetInteger("action", lastMove);
        baseAnim.actionEnd();
    }
}
