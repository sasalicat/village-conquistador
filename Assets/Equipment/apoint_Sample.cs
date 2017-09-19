using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class apoint_Sample : MonoBehaviour,CDEquipment {
    sbyte index;
    public bool CanUse
    {
        get
        {
            return true;
        }
    }

    public uint Consumption
    {
        get
        {
            return 0;
        }
    }

    public bool Designated
    {
        get
        {
            return true;
        }
    }

    public sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }

    public sbyte No
    {
        get
        {
            return 33;
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
    public float TimeLeft
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }
    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        
    }

    public void setTime(float time)
    {
        
    }

    public void trigger(Dictionary<string, object> args)
    {
        Debug.Log("在sample中traget是:"+args["Traget"]);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
