using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lr_skill_buff : Buff
{
    public override float Duration
    {
        get
        {
            return 2;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis, Dictionary<string, object> args)
    {
        Debug.Log("buffForTask onInit!!!!!");
        if (Repetitive == null)
        {
            role.canMove = false;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.canMove = true;
        this.transform.GetComponent<Controler>().addBuffByNo(6);
    }

    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        //Debug.Log("buff ing");
    }
}
