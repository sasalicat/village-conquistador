﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsSample : MonoBehaviour,CDEquipment {

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
            throw new NotImplementedException();
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
            return 10;
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
        
    }

    public void setTime(float time)
    {
        
    }

    public void trigger(Dictionary<string, object> args)
    {
        NetManager.createObstacle(gameObject, transform.position, 1);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
