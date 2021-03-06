﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fb_skill : MonoBehaviour, CDEquipment
{
    public const float CD = 5f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    private AnimatorTable animator;

    public fb_atk atk;

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 41;
        }
    }

    public sbyte selfIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }

    public sbyte Kind//本技能属于主动技能所以kind为 PASSIVE_SKILL
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
    }
    public float TimeLeft
    {
        get
        {
            return CDTime;
        }

        set
        {
            CDTime = value;
        }
    }
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0 && Consumption < selfState.nowMp);//如果CDTime小於0代表技能可以使用
        }
    }
    public uint Consumption
    {
        get
        {
            return 15;//因為是攻擊所以無消耗
        }
    }

    public bool Designated
    {
        get
        {
            return false;
        }
    }



    //----------------------------------------------------------------------
    public void Start()
    {
        

    }

    void Update()
    {

    }

    public void trigger(Dictionary<string, object> args)
    {
        CDTime = CD;
        atk = this.GetComponent(typeof(fb_atk)) as fb_atk;
        atk.CDTime = 0;

        animator.SkillStart();
    }


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.selfState = state;
        this.animator = anim;
    }
}

