using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lmbx_skill_buff : Buff
{
    public double speed;
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
            Debug.Log("onInit Enter Null");
            role.Accelerate += 30;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {

        role.Accelerate -= 30;
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        //Debug.Log("buff ing");
        RoleState r = this.GetComponent<RoleState>();
        role.BeenTreat(this.gameObject, (int)(r.maxHp * 0.03));
        Debug.Log("222222222" + (int)(r.maxHp * 0.03));
    }

    // Update is called once per frame
    void Update()
    {

    }
}