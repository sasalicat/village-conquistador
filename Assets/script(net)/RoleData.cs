using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleData{
    public byte roleKind;
    public List<int> equipmentIdList;
    public RoleData(byte rolekind)
    {
        this.roleKind = rolekind;
    }

}
