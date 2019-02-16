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

    public const int ACTION_NORMAL = 0;
    public const int ACTION_STIFF = 1;
    public const int ACTION_THROW = 2;
    public const int ACTION_ATTACK = -1;
    public Animator lefthand;
    public Animator righthand;
    public Animator eyes;
    public Animator mouse;
    public Animator weapon;
    public Animator face;
    public withNone onActionEnd;
    public weapon_Anim weaponAnim;

    public float stiffTime = 0;

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
    public void action(int actionNo)
    {
        switchWeaponFalse();
        animator.SetInteger("actionNo", actionNo);
        
        //Debug.Log("actionEnd");
    }
    public void actionEnd()
    {
        switchWeaponTrue();
        animator.SetInteger("actionNo",0);
        if (onActionEnd != null)
        {
            onActionEnd();
            onActionEnd = null;
        }
    }
    public void expression(int exprNo)//切換角色的表情
    {
        eyes.SetInteger("exprNo", exprNo);
        mouse.SetInteger("exprNo", exprNo);
    }
    public void switchWeaponFalse()
    {
        weapon.gameObject.SetActive(false);
        lefthand.gameObject.SetActive(true);
        righthand.gameObject.SetActive(true);
    }
    public void switchWeaponTrue()
    {
        weapon.gameObject.SetActive(true);
        lefthand.gameObject.SetActive(false);
        righthand.gameObject.SetActive(false);
    }
    private void switchWeapon(bool active)
    {
        weapon.gameObject.SetActive(active);
        lefthand.gameObject.SetActive(!active);
        righthand.gameObject.SetActive(!active);
    }
    public void WalkStart()
    {
        if (!weapon.gameObject.active)
        {
            switchWeapon(true);
        }
        weaponAnim.setAction(weapon_Anim.WALK);
        face.SetBool("walk", true);
        //switchWeapon(false);
    }
    public void WalkEnd() {
        weaponAnim.setAction(weapon_Anim.NORMAL);
        face.SetBool("walk",false);
    }
    public new void AttackStart()
    {
        if (!weapon.gameObject.active)
        {
            switchWeapon(true);
        }
        weaponAnim.setAction(weapon_Anim.ATTACK);
        face.SetBool("walk", false);
    }
    public void AttackStart(withNone callBack)//實際上動畫樹里並沒有attack,這裡在做的是讓武器動畫運作,并從武器那邊接收callback信號
    {
        AttackStart();
        onActionEnd = callBack;

    }
    public void AttackEnd()
    {
        if (onActionEnd != null)
        {
            onActionEnd();
            onActionEnd = null;
        }
    }

    public override void StiffStart()
    {
        Debug.Log("stiff 開始!!! 我是:"+name);
        if (weapon.gameObject.active)
        {
            switchWeapon(false);
        }
        //damage d= (damage)args["Damage"];
        //Timer.main.logInTimer(checkStiff);//計時把表情切回來
        expression(1);//痛苦的表情
        animator.SetInteger("actionNo", ACTION_STIFF);
        //stiffTime = d.stiffTime;
    }
    public override void StiffEnd()
    {
        if (!weapon.gameObject.active)
        {
            switchWeapon(true);
        }
        Debug.Log("stiff 結束... 我是:" + name);
        expression(0);
        animator.SetInteger("actionNo", ACTION_NORMAL);
    }
    public void checkStiff(float time)
    {
        if (stiffTime <= 0) {
            expression(0);
        }
        Timer.main.loginOutTimer(checkStiff);
    }   
    protected override void Start()
    {
        base.Start();
        Debug.Log(name+"de左手為:" + transform.Find("左手"));
        lefthand = transform.Find("左手").GetComponent<Animator>();
        righthand = transform.Find("右手").GetComponent<Animator>();
        face = transform.Find("face").GetComponent<Animator>();
        mouse = face.transform.Find("mouse").GetComponent<Animator>();
        
        eyes = face.transform.Find("eyes").GetComponent<Animator>();
        weapon = transform.Find("weapon").GetComponent<Animator>();
        animator = GetComponent<Animator>();
        weaponAnim = weapon.GetComponent<weapon_Anim>();
        //KBControler controler = GetComponent<KBControler>();
        //controler.After_take_damage += StiffStart;
    }

}
