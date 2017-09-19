using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porcelain : MonoBehaviour, CDEquipment
{
    public const float CD = 0.5f;//0.5f;  5f
    public const int BaseDamage = 0;
    public const float BaseStiff = 0f;

    public float CDTime = 0;
    public sbyte index;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    AnimatorTable anim;
    damage damage1;

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
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
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

    public void trigger(Dictionary<string, object> args)
    {

        selfState.TakeDamage(damage1);
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        damage1 = new damage(2, 1, 0, false, true, state.gameObject);
        selfState = state;
    }
}