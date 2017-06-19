using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleState : MonoBehaviour {
    protected interface state
    {
        void takedamage(damage damage);
        void beenTreat(int num, GameObject from);
        void onUpdate();
        bool canRota
        {
            get;
        }
        bool canMove{
            get;
        }
        bool canAction
        {
            get;
        }

    }
    public class normal : state
    {
        private RoleState role;
        public normal(RoleState rs)
        {
            role = rs;
        }

        public bool canAction
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool canMove
        {
            get
            {
                return true;
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
            if (role.canBetreat)
            {
                //這裡應有觸發治療事件
            }
        }

        public void onUpdate()
        {
            throw new NotImplementedException();
        }

        public void takedamage(damage damage)
        {
            throw new NotImplementedException();
        }
    }
    public unit selfdata;
    public KBControler control;
    public AnimatorTable anima;
    protected state nowState;
    protected List<state> StateTable=new List<state>();
//真實屬性------------------------------------------------------
    public int maxHp;
    public int nowHp;
    public float nowMp;
    public float nowStiff;//当前硬直时间
    public float nowConversely;//当前倒地时间
    public float energyRecover_Second;
    public float speed;
    //實時狀態-----------------------------------------------------
    //狀態屬性
    public bool immune_attack=false;//是否為攻擊免疫
    public bool immune_skill=false;//是否為特殊傷害免疫
    public bool nonlife=false;//是否為場上靜態物體
    public bool fastened=false;//靜態物體是否固定在位置上
    public bool tools=false;//是否為道具
    public bool canBeExiled=true;//是否可被放逐
    public bool canBeStiff=true;//是否可被硬直
    public bool canBeConvesly=true;//是否可被击倒
    public bool canBeKill=true;//可以被杀死
    public bool canBetreat=true;//可以被治疗

    public bool conversely=false;//是否為倒地
    public bool alive=true;//是否存活
    public bool exiled=true;//是否被放逐

    public bool canMove
    {
        get
        {
            return nowState.canMove;
        }
    }
    public bool canRota
    {
        get
        {
            return nowState.canRota;
        }
    }
    public bool canAction
    {
        get
        {
            return nowState.canAction;
        }
    }

    protected void Start()
    {
        selfdata = GetComponent<unit>();
        anima = GetComponent<AnimatorTable>();
        Debug.Log("in Role state");
        maxHp = unit.STAND_HP + unit.STAND_HP * (selfdata.physique / 100);
        nowHp= maxHp;
        nowMp = 0;
        energyRecover_Second = unit.STAND_MP_RECOVER* (((float)selfdata.energyRecover) / 100);
        speed = unit.STAND_SPEED + unit.STAND_SPEED * (((float)selfdata.accelerate) / 100);
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

    }
    public virtual void TakeDamage(damage damage)
    {
        Debug.Log("super");
    }
}
