using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleState : MonoBehaviour {
    public const int NORMAL_NO = 0;
    public const int STIFF_NO = 1;
    public const int CONVERSELY_NO = 2;
    public const int DEAD_NO = 3;
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

        public int StateNo
        {
            get
            {
                throw new NotImplementedException();
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
        Debug.Log("in Role state"+maxHp+nowHp);
        maxHp = unit.STAND_HP + unit.STAND_HP * (selfdata.physique / 100);
        nowHp= maxHp;
        nowMp = 0;
        energyRecover_Second = unit.STAND_MP_RECOVER* (((float)selfdata.energyRecover) / 100);
        speed = unit.STAND_SPEED + unit.STAND_SPEED * (((float)selfdata.accelerate) / 100);
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

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
