using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lang_bx : ContortionData
{
    public lang_bx()
    {
        this.equipmentNos = new List<sbyte> { 46, 47 };
    }

    public override float Duration
    {
        get
        {
            return 15;//5秒后变身结束
        }
    }

    public override void onAbate(RoleState role)
    {
        Debug.Log("controtionSample onAbate");
        role.canBeStiff = true;
        role.Physique -= 100;
    }

    public override void onInit(RoleState role)
    {
        Debug.Log("controtionSample onInit");
        role.Physique += 100 ;
        role.canBeStiff = false;
    }


}