using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetRoleState :RoleState {
    // Use this for initialization
    public sbyte roomNo;
  
    class normal : state
    {

        private NetRoleState role;

        public normal(NetRoleState role)
        {
            this.role = role;
        }
        public void beenTreat(int num, GameObject from)
        {
            throw new NotImplementedException();
        }

        public void takedamage(damage damage)
        {
            if (damage.kind == 1)
            {
                if (!role.immune_attack)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.damageReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                }
            }
            
        }
    }
    void Start () {
        base.Start();
        StateTable.Add(new normal(this));
        nowState = StateTable[0];
        TakeDamage(new damage(1, 100, 1, true, false, gameObject));
        //Debug.Log("getComp:" + GetComponent<RoleState>());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void  TakeDamage(damage damage)
    {
        nowState.takedamage(damage);
    }
    public void changeHp(int value)//修改nowHp,給Player使用,如果是減血量填負數
    {
        nowHp += value;
    }
    public void addstiff(float value)
    {
        //if(value>)
    }
}
