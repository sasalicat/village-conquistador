﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class protect_mask : MonoBehaviour, CDEquipment
{
    public const float CD = 15f;//0.5f;
    public const int BaseDamage = 0;
    public const float BaseStiff = 4f;

    public float CDTime = 0;
    public sbyte index;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    AnimatorTable anim;

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 11;
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
        }
    }

    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
    }
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0&&selfState.nowMp>= Consumption);//如果CDTime小於0代表技能可以使用
        }
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
    public uint Consumption
    {
        get
        {
            return 20;
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


    public void trigger(Dictionary<string, object> args)
    {
        getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標

        //用來創建障礙物
        NetManager.createObstacle(gameObject, origenPlayerPosition, 3,300);
        anim.AttackStart();

        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.anim = anim;
        selfState = state;
    }
}
