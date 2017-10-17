using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageReduce : MonoBehaviour
{
    public float CDTime = 0;
    public sbyte index;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    getVector getVector;
    private AnimatorTable animator;

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
            return EquipmentTable.NO_TRIGGER;
            //return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        //Debug.Log(CDTime + "left");
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




    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        this.selfState = state;
        this.animator = anim;
        selfState.damageReduce += 18;
    }
}
