﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mg_atk : MonoBehaviour,CDEquipment
{
    public const float CD = 0.5f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public float CDTime = 0; 
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    public Text Label;
    getVector getVector;

//實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 0;
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
        //Debug.Log(CDTime + "left");
        CDTime -= time;//減少CD時間
    }
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
            //return true;
        }
    }
    public uint Consumption
    {
        get
        {
            return 0;//因為是攻擊所以無消耗
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
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        Vector3 tragetPos =getVector.getOriginalInitPoint(origenPlayerPosition,mousePosition,new Vector3(0,-1,0));//獲得相對座標
        //制造子弹物件
        GameObject newone= Instantiate(missilePraf, tragetPos, transform.rotation);
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();
        missile.Creater = gameObject;
        //创建伤害物件
        int num = (int)(BaseDamage + BaseDamage * ((float)selfState.selfdata.power / 100));
        float stiff = BaseStiff+BaseStiff * (((float)selfState.selfdata.stiffable)/100);
        missile.Damage = new damage(1, num,stiff,false,false,gameObject);

        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);
    }


    public void onInit(MissileTable table,RoleState state,AnimatorTable anim)
    {
        //初始化赋值
        Debug.Log("魔术师扑克牌初始化 state:"+state);
        missilePraf = table.MissileList[0];
        this.selfState = state;
        getVector= GameObject.Find("keyTabel").GetComponent<getVector>();
    }
}
