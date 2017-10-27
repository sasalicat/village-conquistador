using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameBuff : Buff {
    private float nexthurt = 1;
    private GameObject show;
    public GameObject causer;
    public override float Duration
    {
        get
        {
            return 5;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable)
    {
        if (Repetitive != null)
        {
            if (Repetitive.Length > 0)
            {
                Repetitive[0].timeLeft = 5;
                return false;
            }
            else
            {
                show=Instantiate(misTable.MissileList[34], role.transform.position, role.transform.rotation,role.transform);
                return true;
            }
        }
        show = Instantiate(misTable.MissileList[34], role.transform.position, role.transform.rotation, role.transform);

        return true;
    }

    public override void onRemove(RoleState role)
    {
        Destroy(show);
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        nexthurt -= timeBetween;
        if (nexthurt < 0)
        {
            role.TakeDamage(new damage(2,15,0,false,false,causer));
            nexthurt +=1;
        }
    }


}
