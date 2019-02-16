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


    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable mis, Dictionary<string, object> args)
    {
        Debug.Log("buffForTask onInit");

        if (Repetitive != null && Repetitive.Length <= 5)
        {
            Debug.Log("add buff");
            role.SpeedScale -= 0.15f;
            return true;
        }
        return false;
    }

    public override void onRemove(RoleState role)
    {

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