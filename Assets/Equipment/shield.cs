using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour, CDEquipment
{
    public const float CD = 4f;//0.5f;
    public const int BaseDamage = 0;
    public const float BaseStiff = 4f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    AnimatorTable anim;
    damage damage;
    GameObject shieldGameObject = null;

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
            return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
        if (CDTime <= 0)
        {
            if (shieldGameObject == null)
                shieldGameObject = Instantiate(missilePraf, transform.position, transform.rotation, transform);
            CDTime = 4;
        }
    }

    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
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
        damage = (damage)args["Damage"];

        if (shieldGameObject != null)
        {
            damage.num = 0;
            damage.stiffTime = 0;
            Debug.Log("123123123123123123~~~~");
            Destroy(shieldGameObject);
        }


        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        missilePraf = table.MissileList[9];
    }
}