using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawDefense : MonoBehaviour,CDEquipment {
    public const int BaseDamage = 100;
    public const float BaseStiff = 0.5f;
    public const float CD = 0.5f;
    sbyte index;
    private float CDTime = 0;
    private RoleState state;
    private GameObject missile;

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

    public sbyte Kind
    {
        get
        {
            return CodeTable.TAKE_DAMAGE;
        }
    }

    public sbyte No
    {
        get
        {
            return 1;
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
    public void setTime(float time)
    {
        if (CDTime > 0)
        {
            CDTime -= time;
        }
    }

    public void onInit(MissileTable table, RoleState state)
    {
        this.missile = table.MissileList[1];
        this.state = state; 
    }

    public void trigger(Dictionary<string, object> args)
    {
        if (CDTime<=0) {
            Vector3 pos = (Vector3)args["PlayerPosition"];
            int realNum = (int)(BaseDamage + BaseDamage * (((float)state.selfdata.power) / 100));
            float realStiff = BaseStiff + BaseStiff * (((float)state.selfdata.stiffable) / 100);
            damage damage = new damage(2, realNum, realStiff, false, false, gameObject);
            GameObject newone = Instantiate(missile, pos, this.transform.rotation);
            Missile mis = newone.GetComponent<Missile>();
            mis.Damage = damage;
            mis.Creater = gameObject;

            CDTime = CD;
        }
    }
    public void Update()//这里写法不对
    {
        CDTime -= Time.deltaTime;
    }
  

}
