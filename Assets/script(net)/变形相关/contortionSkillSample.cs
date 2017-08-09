using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contortionSkillSample : MonoBehaviour,CDEquipment {
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
            return false;
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
            return 34;
        }
    }
    sbyte index;
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
        
    }

    public void setTime(float time)
    {
        
    }

    public void trigger(Dictionary<string, object> args)
    {
        gameObject.GetComponent<Controler>().distortionByNo(0);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
