using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_bx_buff : Buff
{
    public double speed;
    public override float Duration
    {
        get
        {
            return 5;
        }
    }


    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis)
    {
        Debug.Log("buffForTask onInit");
        //if(Repetitive <= )
        if (Repetitive != null && Repetitive.Length <= 5)
        {
            role.speed = 0.9f * role.speed;
            
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.canMove = true;
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        //Debug.Log("buff ing");
    }

    // Update is called once per frame
    void Update()
    {

    }
}