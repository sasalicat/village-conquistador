using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_skill_buff_two : Buff
{
    public override float Duration
    {
        get
        {
            return 3;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis, Dictionary<string, object> args)
    {
        Debug.Log("buffForTask onInit!!!!!");
        if (Repetitive == null)
        {
            role.SpeedScale -= 0.5f;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.SpeedScale += 0.5f;
    }

    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        //Debug.Log("buff ing");
    }
}
