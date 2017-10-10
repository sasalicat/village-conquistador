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
            return 20;
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
        if(1.3 * role.nowHp < role.maxHp)
        {
            role.nowHp += (int)0.3 * role.nowHp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}