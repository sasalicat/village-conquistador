using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buffForTask : Buff
{
    public override float Duration
    {
        get
        {
            return 8;
        }
    }


    public override bool onInit(RoleState role, Buff[] Repetitive)
    {
        Debug.Log("buffForTask onInit");
        if (Repetitive == null)
        {
            Debug.Log("onInit Enter Null");
            role.canMove = false;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.canMove = true;
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        base.onIntarvel(role, timeBetween);
        Debug.Log("buff ing");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
