using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chillbuff : Buff {
    public override float Duration
    {
        get
        {
            return 2;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable, Dictionary<string, object> args)
    {
        if (Repetitive != null)
        {
            if (Repetitive.Length > 0)
            {
                Repetitive[0].timeLeft = 2;
                return false;
            }
            else
            {
                role.SpeedScale -= 0.5f;
                return true;
            }
        }
        role.SpeedScale -= 0.5f;
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.SpeedScale += 0.5f;
    }


}
