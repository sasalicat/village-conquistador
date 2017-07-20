using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regeneration : MonoBehaviour, Equipment
{
    public sbyte index;
    RoleState roleState;
    float time = 2f;
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
            return EquipmentTable.ON_INTERVAL;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.roleState = state;
    }

    public void trigger(Dictionary<string, object> args)
    {
        float interval = (float)args["interval"];
        if ((time -= interval) <= 0)
        {
            roleState.BeenTreat(this.gameObject,(int)(roleState.maxHp * 0.03));
            time = 2;
        }
    }

}