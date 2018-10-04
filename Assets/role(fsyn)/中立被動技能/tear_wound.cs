using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tear_wound : MonoBehaviour,Equipment {
    private sbyte index;
    private const int buffNo = 11;
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
            return 67;
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

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        var eList = state.GetComponent<EquipmentList>();
        var attack = eList.nowHarness.passiveEquipments[EquipmentList.ATK];
        if (attack.GetType().GetInterface("canBeAddition") != null)
        {
            Debug.Log("在onInit中canBeAddition:" + attack);
            ((canBeAddition)attack).onCauseDamage += trigger;
        }
    }

    public void trigger(Dictionary<string, object> args)
    {
        ((GameObject)args["Traget"]).GetComponent<Controler>().addBuffByNo(buffNo);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
