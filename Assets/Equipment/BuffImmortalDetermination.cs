using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffImmortalDetermination : Buff
{
    public override float Duration
    {
        get
        {
            return 10;
        }
    }


    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis)
    {
        Debug.Log("buffForTask onInit");
        if (Repetitive == null)
        {
            role.canBeKill = false;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.canBeKill = true;
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        Debug.Log("buff ing");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
