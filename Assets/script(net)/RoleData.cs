using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData
{
    public sbyte roleKind;
    public List<sbyte> equipmentIdList;
    public RoleData(sbyte rolekind)
    {
        this.roleKind = rolekind;
        equipmentIdList = new List<sbyte>();
        equipmentIdList.Add(0);
    }
    public RoleData(sbyte rolekind, List<sbyte> equipmentIdList)
    {
        this.roleKind = rolekind;
        this.equipmentIdList = equipmentIdList;
    }
}
