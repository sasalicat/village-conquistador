using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flameTauch : MonoBehaviour,Equipment {
    private sbyte index;
    private MissileTable table;
    private RoleState state;
    private AnimatorTable anim;
    public sbyte Kind
    {
        get
        {
            return EquipmentTable.NO_TRIGGER;
        }
    }

    public sbyte No
    {
        get
        {
            return 64;
        }
    }

    public sbyte selfIndex
    {
        get
        {
            return index;
        }

        set
        {
            index = value;
        }
    }
    public void onHitTraget(GameObject obj)
    {
        var control= obj.GetComponent<Controler>();
        if (control!=null)
        {
            control.addBuffByNo(9);
        }
    }
    public void aftCreateMis(addibleMissile traget, bool original)
    {
        traget.onHit += onHitTraget;
    }
    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.table = table;
        this.state = state;
        this.anim = anim;
        var list = state.GetComponent<EquipmentList>().nowHarness;
        Debug.Log("list.passiveEquipments[0]:" + list.passiveEquipments[0]);
        ((canBeAddition)list.passiveEquipments[0]).onCreateMissile += aftCreateMis;
    }

    public void trigger(Dictionary<string, object> args)
    {
    }
}
