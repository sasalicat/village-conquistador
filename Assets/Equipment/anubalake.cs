﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anubalake : MonoBehaviour, CDEquipment
{
    public const float CD = 10;//0.5f;
    public const int BaseDamage = 200;
    public const float BaseStiff = 4f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    public Text Label;

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 4;
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
    public bool CanUse
    {
        get
        {
            return (CDTime <= 0&&Consumption<=selfState.nowMp);//如果CDTime小於0代表技能可以使用
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
            return 10;//因為是攻擊所以無消耗
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
        //制造子弹物件
        GameObject newone = Instantiate(missilePraf, tragetPos, transform.rotation);
        newone.transform.up = mousePosition - origenPlayerPosition;
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = gameObject;
        //创建伤害物件
        int num = Attribute.GetSpecialDamageNum(BaseDamage, selfState.Skill);
       
        missile.Damage = new damage(1, num, 0, true, true, gameObject);

        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);

    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        missilePraf = table.MissileList[4];
        this.selfState = state;
    }
}
