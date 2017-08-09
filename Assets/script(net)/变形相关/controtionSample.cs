using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controtionSample : ContortionData
{
    public controtionSample()
    {
        this.equipmentNos =new List<sbyte> {2,3 };
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
