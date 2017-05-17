using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleState : MonoBehaviour {
    public interface state
    {
        void takedamage(damage damage);
        void beenTreat(int num, GameObject from);
    }
    public class normal : state
    {
        private RoleState role;
        public normal(RoleState rs)
        {
            role = rs;
        }
        public void beenTreat(int num, GameObject from)
        {
            if (role.canBetreat)
            {
                //這裡應有觸發治療事件
            }
        }

        public void takedamage(damage damage)
        {
            throw new NotImplementedException();
        }
    }
    public unit selfdata;
    public Controler control;
    public AnimatorTable anima;
//真實屬性------------------------------------------------------
    public int maxHp;
    public int nowHp;
    public float nowMp;
    public float energyRecover_Second;
    public float speed;
    //實時狀態-----------------------------------------------------
    //狀態屬性
    public bool immune_attack;//是否為攻擊免疫
    public bool immune_skill;//是否為特殊傷害免疫
    public bool nonlife;//是否為場上靜態物體
    public bool fastened;//靜態物體是否固定在位置上
    public bool tools;//是否為道具
    public bool canBeExiled;//是否可被放逐
    public bool canBeStiff;//是否可被硬直
    public bool canBeConvesly;//是否可被击倒
    public bool canBeKill;//可以被杀死
    public bool canBetreat;//可以被治疗

    public bool conversely;//是否為倒地
    public bool alive;//是否存活
    public bool exiled;//是否被放逐

    protected void Start()
    {
        Debug.Log("in Role state");
        maxHp = unit.STAND_HP + unit.STAND_HP * (selfdata.physique / 100);
        nowHp= maxHp;
        nowMp = 0;
        energyRecover_Second = unit.STAND_MP_RECOVER* (((float)selfdata.energyRecover) / 100);
        speed = unit.STAND_SPEED + unit.STAND_SPEED * (((float)selfdata.accelerate) / 100);
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

    }
    public void TakeDamage(damage damage)
    {

    }
}
