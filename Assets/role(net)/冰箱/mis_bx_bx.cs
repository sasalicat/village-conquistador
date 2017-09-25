using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_bx_bx : ContortionData
{
    public mis_bx_bx()
    {
        this.equipmentNos = new List<sbyte> {  };
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
        role.canAction = true;
        role.canMove = true;
        role.canRota = true;
    }

    public override void onInit(RoleState role)
    {
        role.canAction = false;
        role.canMove = false;
        role.canRota = false;
        Debug.Log("controtionSample onInit");
    }



}