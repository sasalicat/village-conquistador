using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Mobile : AnimatorTable{
    public delegate void withNone();

    public const sbyte LEFT_1 = 0;
    public const sbyte LEFT_2 = 1;
    public const sbyte LEFT_3 = 2;
    public const sbyte RIGHT_1 = 3;
    public const sbyte RIGHT_2 = 4;
    public const sbyte RIGHT_3 = 5;

    public const int EXPR_NORMAL = 0;
    public const int EXPR_PAIN = 1;
    public const int EXPR_ANGLE = 2;
    public Animator lefthand;
    public Animator righthand;
    public Animator eyes;
    public Animator mouse;
    public withNone onActionEnd;

    // Use this for initialization
    public void changeToL1()
    {
        changehand(LEFT_1);
    }
    public void changeToL2()
    {
        changehand(LEFT_2);
    }
    public void changeToL3()
    {
        changehand(LEFT_3);
    }
    public void changeToR1()
    {
        changehand(RIGHT_1);
    }
    public void changeToR2()
    {
        changehand(RIGHT_2);
    }
    public void changeToR3()
    {
        changehand(RIGHT_3);
    }
    public void changehand(sbyte handNo)
    {
        int leftRight = handNo / 2;
        int no = handNo;
        no = no % 3;
        if (leftRight == 0)
            lefthand.SetInteger("handNo",no);
        else
            righthand.SetInteger("handNo", no);
    }
    public void action(int actionNo,withNone callback)
    {
        onActionEnd += callback;
        animator.SetInteger("actionNo",actionNo);
        Debug.Log("actionEnd");
    }
    public void actionEnd()
    {
        animator.SetInteger("actionNo",0);
        onActionEnd();
        onActionEnd = null;
    }
    public void expression(int exprNo)//切換角色的表情
    {
        eyes.SetInteger("exprNo", exprNo);
        mouse.SetInteger("exprNo", exprNo);
    }


}
