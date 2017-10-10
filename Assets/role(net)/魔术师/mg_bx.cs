using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mg_bx : ContortionData
{
    public mg_bx()
    {
        this.equipmentNos = new List<sbyte> { };
    }

    public override float Duration
    {
        get
        {
            return 5;//5秒后变身结束
        }
    }

    public override void onAbate(RoleState role)
    {
        Debug.Log("controtionSample onAbate");

    }

    public override void onInit(RoleState role)
    {
        Debug.Log("controtionSample onInit");

    }


}