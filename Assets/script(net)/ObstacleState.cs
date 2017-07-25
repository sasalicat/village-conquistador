using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class ObstacleState : RoleState {//相当于控制器和State结合在同一个脚本
    public Entity entity;
    public  GameObject Creater;
    public int Kind;
    public int nullCallTimes = 0;
    public List<sbyte> sbyteCalldatas = new List<sbyte>();
    class obs_normal : state
    {
        private ObstacleState obs;
        public bool canAction
        {
            get
            {
                return false;
            }
        }

        public bool canMove
        {
            get
            {
                return false;
            }
        }

        public bool canRota
        {
            get
            {
                return false;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            return;
        }

        public void onUpdate()
        {
            while (obs.nullCallTimes > 0)
            {
                Debug.Log("method null从服务器呼叫 times:"+ obs.nullCallTimes);
                obs.methodNull();
                obs.nullCallTimes--;
            }
            while (obs.sbyteCalldatas.Count > 0)
            {
                obs.methodSbyte(obs.sbyteCalldatas[0]);
                obs.sbyteCalldatas.RemoveAt(0);
            }
        }

        public void takedamage(damage damage)
        {
            if (damage.damager.GetComponent<NetPlayerControler>() != null)
            {
                obs.entity.cellCall("reduceHp", new object[] { (short)damage.num });
            }
        }
        public obs_normal(ObstacleState obs)
        {
            this.obs = obs;
        }
    }
    // Use this for initialization
      override protected void Start () {
        base.Start();
        StateTable = new List<state>();
        nowState = new obs_normal(this);
    }
	
	// Update is called once per frame
	protected void Update () {
        nowState.onUpdate();
	}
    public virtual void methodNull()
    {
        Debug.Log("父類別methodnull");
    }
    public virtual void methodSbyte(sbyte data)
    {

    }
    public void callMethodNull()
    {
        if (Creater.GetComponent<NetRoleState>().islocal)
        {
            Debug.Log("呼叫服务器nullmethod");
            entity.cellCall("method_Null");
        }
    }
    public void callMethodSbyte(sbyte data)
    {
        if (Creater.GetComponent<NetRoleState>().islocal)
        {
            entity.cellCall("methodSbyte",new object[] {data});
        }
    }
    public void DestoryObjInServer()
    {
        entity.cellCall("DestoryEntity");
    }
}
