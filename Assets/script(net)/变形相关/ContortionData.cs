using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContortionData{
    public List<sbyte> equipmentNos = new List<sbyte>();
    public abstract void onInit(RoleState role);
    public abstract void onAbate(RoleState role);
    public abstract float Duration
    {
        get;
    }
}
