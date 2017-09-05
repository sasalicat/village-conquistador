﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoleState : MonoBehaviour {
    public const int NORMAL_NO = 0;
    public const int STIFF_NO = 1;
    public const int CONVERSELY_NO = 2;
    public const int DEAD_NO = 3;
    public bool hasStart = false;
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
        int StateNo
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
                return true;
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
                return true;
            }
        }

        public int StateNo
        {
            get
            {
                return NORMAL_NO;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            if (role.canBetreat)
            {
                if (role.control.On_Been_Treat != null)
                {
                    Dictionary<string, object> Arg = new Dictionary<string, object>();
                    Arg["Treater"] = from;
                    Arg["Num"] = num;
                    Arg["randomPoint"] = UnityEngine.Random.Range(0,100);
                    role.control.On_Been_Treat(Arg);
                }
                
            }
        }

        public void onUpdate()
        {
            
        }

        public void takedamage(damage damage)
        {
            if (damage.kind == 1)
            {
                if (!role.immune_attack)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.damageReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    Debug.Log("send backage");
                }
            }
            if (role.control.On_Take_Damage != null)
            {
                Dictionary<string, object> Arg = new Dictionary<string, object>();
                Arg["PlayerPosition"] = role.transform.position;
                Arg["PlayerEuler"] = role.transform.eulerAngles;
                Arg["DamagerPosition"] = damage.damager.transform.position;
                Arg["Damager"] = damage.damager;
                Arg["randomPoint"] = UnityEngine.Random.Range(0, 100);
                Arg["Damage"] = damage;
                role.control.On_Take_Damage(Arg);
            }
            if (role.control.On_Cause_Damage!=null)
            {
                Dictionary<string, object> Arg = new Dictionary<string, object>();
                Arg["Damage"] = damage;
                Arg["PlayerPosition"] = damage.damager.transform.position;
                Arg["TragetPosition"] = role.transform.position;
                Arg["randomPoint"] = UnityEngine.Random.Range(0, 100);
                Arg["Traget"] = role.gameObject;
                role.control.On_Cause_Damage(Arg);
            }
        }
    }
    public unit selfdata;
    public Controler control;
    public AnimatorTable anima;
    protected state nowState;
    protected List<state> StateTable=new List<state>();
    public sbyte team;
//真實屬性------------------------------------------------------
    public int maxHp;
    public int nowHp;
    public float nowMp;
    public float nowStiff;//当前硬直时间
    public float nowConversely;//当前倒地时间
    public float energyRecover_Second;
    public float speed;
    public float speedRate = 1;
    //實時狀態-----------------------------------------------------
    //狀態屬性
    private int immune_attack_count = 0;
    public bool immune_attack
    {
        set
        {
            if (value)
                immune_attack_count++;
            else
                immune_attack_count--;
        }
        get
        {
            return immune_attack_count > 0;
        }
    }//是否為攻擊免疫
    private int immune_skill_count = 0;
    public bool immune_skill
    {
        set
        {
            if (value)
                immune_skill_count++;
            else
                immune_skill_count--;
        }
        get
        {
            return immune_skill_count > 0;
        }
    }//是否為特殊傷害免疫
    public readonly bool nonlife=false;//是否為場上靜態物體
    public readonly bool fastened=false;//靜態物體是否固定在位置上
    public readonly bool tools=false;//是否為道具
    private int canExiledCount=1;
    public bool canBeExiled
    {
        set
        {
            if (value)
                canExiledCount++;
            else
                canExiledCount--;
        }
        get
        {
            return canExiledCount > 0;
        }
    }//是否可被放逐
    private int canStiffCount=1;
    public bool canBeStiff
    {
        set
        {
            if (value)
                canStiffCount++;
            else
                canStiffCount--;
        }
        get
        {
            return canStiffCount > 0;
        }
    }//是否可被硬直
    private int canConverslyCount=1;
    public bool canBeConvesly
    {
        set
        {
            if (value)
                canConverslyCount++;
            else
                canConverslyCount--;
        }
        get
        {
            return canConverslyCount > 0;
        }
    }//是否可被击倒
    private int canKillCount=1;
    public bool canBeKill
    {
        set
        {
            if (value)
                canKillCount++;
            else
                canKillCount--;
        }
        get
        {
            return canKillCount > 0;
        }
    }//可以被杀死
    private int canTreatCount=1;
    public bool canBetreat
    {
        set
        {
            if (value)
                canTreatCount++;
            else
                canTreatCount--;
        }
        get
        {
            return canTreatCount > 0;
        }
    }//可以被治疗

    //public bool conversely=false;//是否為倒地
    //public bool alive=true;//是否存活
    //public bool exiled=true;//是否被放逐
    private int canMoveCount = 1;
    public bool canMove
    {
        set
        {
            if (value)
                canMoveCount++;
            else
                canMoveCount--;
        }
        get
        {
            Debug.Log("nowState is" + nowState+"lengh"+ StateTable.Count+"  "+hasStart);
            return nowState.canMove&&canMoveCount>0;
        }
    }
    private int canRotaCount = 1;
    public bool canRota
    {
        set
        {
            if (value)
                canRotaCount++;
            else
                canRotaCount--;
        }
        get
        {
            return nowState.canRota&&canRotaCount>0;
        }
    }
    private int canActionCount = 1;
    public bool canAction
    {
        set
        {
            if (value)
                canActionCount++;
            else
                canActionCount--;
        }
        get
        {
            return nowState.canAction&& canActionCount>0;
        }
    }
    public int nowStateNo
    {
        get
        {
            return nowState.StateNo;
        }
    }
    virtual protected void Start()
    {
        selfdata = GetComponent<unit>();
        anima = GetComponent<AnimatorTable>();
        control = GetComponent<Controler>();
        Debug.Log("in Role state"+maxHp+nowHp);
        maxHp = unit.STAND_HP + unit.STAND_HP * (selfdata.physique / 100);
        nowHp= maxHp;
        nowMp = 0;
        energyRecover_Second = unit.STAND_MP_RECOVER* (((float)selfdata.energyRecover) / 100);
        speed = unit.STAND_SPEED + unit.STAND_SPEED * (((float)selfdata.accelerate) / 100);

        StateTable.Add(new normal(this));
        nowState = StateTable[0];
        hasStart = true;
        Debug.Log("in start nowState is" + nowState);
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

    }
    virtual protected void Update()
    {
        nowState.onUpdate();
    }
    public virtual void TakeDamage(damage damage)
    {
        nowState.takedamage(damage);
        Debug.Log("super");
    }
    public virtual void BeenTreat(GameObject treater,int num)
    {
        nowState.beenTreat(num, treater);
    }
    public void recoverMP(float num)
    {
        if (nowMp + num < unit.STAND_MP)
        {
            nowMp += num;
        }
        else
        {
            nowMp = unit.STAND_MP;
        }
    }
}
