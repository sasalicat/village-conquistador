﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mis_nq_buff : Buff
{
    private GameObject missilePraf;
    public override float Duration
    {
        get
        {
            return 3;
        }
    }


    public override bool onInit(RoleState role, Buff[] Repetitive,MissileTable misTable)
    {
        if (Repetitive == null)
        {
            Debug.Log("onInit Enter Null");
            role.immune_attack = true;
        }
        getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();

        //制造子弹物件
        //Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        GameObject newone = Instantiate(misTable.MissileList[18], this.transform.position, transform.rotation);

        newone.transform.parent = this.transform;


        //newone.transform.up = -direction;
        //修改子弹物件携带的子弹脚本
        Missile missile = newone.GetComponent<Missile>();

        return true;
    }

    public override void onRemove(RoleState role)
    {
        Destroy(missilePraf);
    }
    public override void onIntarvel(RoleState role, float timeBetween)
    {
        
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}

