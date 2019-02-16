using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff_moveless : Buff {
    private float duration =0;
    public override float Duration
    {
        get
        {
            return duration;
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
        }
        role.canMove = false;
        this.duration = (float)args["time"];
        this.timeLeft = this.duration;
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.canMove = true;
    }


}
