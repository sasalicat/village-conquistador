using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lang_bx : ContortionData
{
    public lang_bx()
    {
        this.equipmentNos = new List<sbyte> { 48, 49 };
    }

    public override float Duration
    {
        get
        {
            return 20;//5秒后变身结束
        }
    }

    public override void onAbate(RoleState role)
    {
        Debug.Log("controtionSample onAbate");
        role.canBeStiff = true;
        role.Physique -= 30;
        role.Power -= 30;
        role.Accelerate -= 30;
        role.transform.localScale *= 0.5f;
    }

    public override void onInit(RoleState role)
    {
        Debug.Log("controtionSample onInit");
        role.Physique += 30 ;
        role.Power += 30;
        role.canBeStiff = false;
        role.transform.localScale *= 2;
        role.Accelerate += 30;
    }


}