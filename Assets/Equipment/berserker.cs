using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berserker : MonoBehaviour,Equipment {
    public sbyte index;
    float percent;
    float beforePercent;
    RoleState roleState;
    bool isFist = true;
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
            return EquipmentTable.ON_HP_CHANGE;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.roleState = state;
    }

    public void trigger(Dictionary<string, object> args)
    {
        percent = (float) args["Percent"];//小數的百分比
        
        if (isFist)
        {
            beforePercent = percent;
            roleState.selfdata.power += (int)((1 - beforePercent) * 100);
            isFist = false;
        }else
        {
            roleState.selfdata.power += (int)((beforePercent - percent) * 100);
            beforePercent = percent;
        }
        
    }
    
}
