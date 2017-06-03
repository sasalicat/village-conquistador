using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetRoleState :RoleState {
    // Use this for initialization
    public sbyte roomNo;
    public const int NORMAL_NO= 0;
    public const int STIFF_NO = 1;
    public const int CONVERSELY_NO = 2;
    public const int DEAD_NO = 3;
    interface state_net :state
    {
        void hurt(damage damage);
    }

    class normal : state_net
    {

        private NetRoleState role;

        public bool canRota
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
            Debug.Log("role takedamage role kind is"+damage.kind);
            if (damage.kind == 1)
            {
                if (!role.immune_attack)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.damageReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                    Debug.Log("send backage");
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                    Debug.Log("send backage");
                }
            }
            
        }
        public void hurt(damage damage)
        {
            Debug.Log("enter hurt");
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else if (role.canBeKill)//死亡
            {
                role.nowHp = 0;
                role.changeState(3);
                role.anima.ConverselyStart();
            }
          
                if (damage.makeConversaly && role.canBeConvesly)//如果会被击倒则不做硬直判定
                {
                    role.nowConversely = unit.STAND_CONVESLY_TIME;
                    role.changeState(CONVERSELY_NO);
                    role.changeState(3);
                }
                else if (role.canBeStiff)
                {
                    Debug.Log("enter stiff");
                    role.nowStiff = damage.stiffTime;
                    role.changeState(STIFF_NO);
                    role.anima.StiffStart();
                }
            
        }

        public void onUpdate()
        {
            
        }
    }
    class stiff : state_net
    {
        private NetRoleState role;
        public stiff(NetRoleState role)
        {
            this.role = role;
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
            throw new NotImplementedException();
        }

        public void hurt(damage damage)
        {
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else
            {
                role.nowHp = 0;
            }
            if (damage.makeConversaly && role.canBeConvesly)//如果会被击倒则不做硬直判定
            {
                role.nowConversely = unit.STAND_CONVESLY_TIME;
                role.changeState(CONVERSELY_NO);
                role.changeState(3);
            }
            else if (damage.stiffTime > role.nowStiff)
            {
                role.nowStiff = damage.stiffTime;
            }
        }

        public void onUpdate()
        {
            if (role.nowStiff <= 0)
            {
                role.changeState(0);
                role.anima.StiffEnd();
            }
            else {
                role.nowStiff -= Time.deltaTime;
            }

        }

        public void takedamage(damage damage)
        {
            Debug.Log("role takedamage role kind is" + damage.kind);
            if (damage.kind == 1)
            {
                if (!role.immune_attack)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.damageReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                    Debug.Log("send backage");
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    role.control.Role_onTakeDamage(damage);
                    Debug.Log("send backage");
                }
            }

        }
    }
    class conversely : state_net
    {
        private NetRoleState role;
        public conversely(NetRoleState role)
        {
            this.role = role;
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
            throw new NotImplementedException();
        }

        public void hurt(damage damage)
        {
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else
            {
                role.nowHp = 0;
            }
            
        }

        public void onUpdate()
        {
            if (role.nowConversely <= 0)
            {
                role.changeState(0);
                role.anima.ConverselyEnd();
            }
            else
            {
                role.nowConversely -= Time.deltaTime;
            }

        }

        public void takedamage(damage damage)
        {
            if (damage.hitConversely)
            {
                if (damage.kind == 1)
                {
                    if (!role.immune_attack)
                    {
                        damage.num -= (int)(damage.num * (((float)role.selfdata.damageReduce) / 100));
                        damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                        role.control.Role_onTakeDamage(damage);
                        Debug.Log("send backage");
                    }
                }
                else if (damage.kind == 2)
                {
                    if (!role.immune_skill)
                    {
                        damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                        damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                        role.control.Role_onTakeDamage(damage);
                        Debug.Log("send backage");
                    }
                }

            }
        }
    }
    class died : state_net
    {
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

        public void hurt(damage damage)
        {
            return;
        }

        public void onUpdate()
        {
            return;
        }

        public void takedamage(damage damage)
        {
            return;
        }
    }

    new void  Start () {
        base.Start();
        StateTable = new List<state>();
        //重置父類別的StateTable
        StateTable.Add(new normal(this));
        StateTable.Add(new stiff(this));
        StateTable.Add(new conversely(this));

        //初始为normal
        nowState = StateTable[0];
        //TakeDamage(new damage(1, 100, 1, true, false, gameObject));
        //Debug.Log("getComp:" + GetComponent<RoleState>());
    }
	
	// Update is called once per frame
	void Update () {
        nowState.onUpdate();
	}
    public override void  TakeDamage(damage damage)
    {
        Debug.Log("net role state");
        nowState.takedamage(damage);
    }
   public void realHurt(damage damage)
    {
        ((state_net)nowState).hurt(damage);
    }
    public void changeState(int no)
    {
        nowState = StateTable[no];
    }
}
