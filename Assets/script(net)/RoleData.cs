using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData{
    public byte roleKind;
    public List<sbyte> equipmentIdList;
    public RoleData(byte rolekind)
    {
        this.roleKind = rolekind;
        equipmentIdList = new List<sbyte>();
        equipmentIdList.Add(0);
    }

}
