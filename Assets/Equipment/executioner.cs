using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class executioner : MonoBehaviour, Equipment
{
    public sbyte index;
    GameObject target;
    RoleState roleState;
    RoleState targetRoleState;
    damage damage1;

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
            return EquipmentTable.ON_CAUSE_DAMAGE;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.roleState = state;
    }

    public void trigger(Dictionary<string, object> args)
    {
        target = (GameObject)args["Traget"];//攻擊目標
        damage1 = (damage)args["Damage"];

        targetRoleState = target.GetComponent<RoleState>();
        int nowHp = targetRoleState.nowHp;
        int maxHp = targetRoleState.maxHp;
        int result = (int)((float)nowHp / maxHp * 100);
        
        if (result <= 20)
        {
            Debug.Log("123123");
            damage1.num *= 2;
        }
    }

}