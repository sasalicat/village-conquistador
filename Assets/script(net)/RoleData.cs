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
    public bool equal(RoleData other)
    {
        if (this.roleKind != other.roleKind)
        {
            return false;
        }
        if (other.equipmentIdList.Count != this.equipmentIdList.Count)
        {
            return false;
        }
        else
        {
            for(int i=0;i<equipmentIdList.Count;i++)
            {
                if (this.equipmentIdList[i] != other.equipmentIdList[i])
                {
                    return false;
                }
            }
        }
        return true;
    }
}
