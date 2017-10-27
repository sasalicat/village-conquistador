using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lajibuff : Buff
{
    const int everytreat=20;
    float treatInterval = 1;
    float nextTreat;
    GameObject effection = null;
    public override float Duration
    {
        get
        {
           return 10;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable)
    {
        if(Repetitive == null)
        {
            effection=Instantiate(misTable.MissileList[35],role.transform.position,role.transform.rotation);
            effection.transform.parent = role.transform;
            return true;
        }
        if ( Repetitive.Length != 0)
        {
            ((lajibuff)(Repetitive[0])).timeLeft = 10;//重置已有buff的持續時間
            return false;//不添加自己
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        if (effection != null)
        {
            Destroy(effection);
        }
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        nextTreat -= timeBetween;
        if (nextTreat < 0)
        {
            role.BeenTreat(null,everytreat);
            nextTreat = treatInterval;
        }
    }

}
