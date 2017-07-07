using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Equipment {
    sbyte selfIndex
    {
        set;
        get;
    }
    sbyte No
    {
        get;
    }
    sbyte Kind
    {
        get;
    }
    void onInit(MissileTable table,RoleState state,AnimatorTable anim);//这个funciton会在EquipmentList第一个Update呼叫
    void trigger(Dictionary<string,object> args);

	
}
