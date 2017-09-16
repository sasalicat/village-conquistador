using System;
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
    class normal : state
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
                   role.control.On_Been_Treat(RoleState.CreateTreatArg(num, from));
                }
                treat(num);
                role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
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
                role.control.On_Take_Damage(RoleState.CreateTakeDamageArg(role,damage));
            }
            if (role.control.On_Cause_Damage!=null)
            {
                role.control.On_Cause_Damage(RoleState.CreateCauseDamageArg(role,damage));
            }
            hurt(damage);
            role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
        }
        private void hurt(damage damage)
        {
            Debug.Log("enter hurt");
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else if (role.canBeKill)//死亡
            {
                role.nowHp = 0;
                role.changeState(DEAD_NO);
                role.anima.ConverselyStart();
                return;

            }

            if (damage.makeConversaly && role.canBeConvesly)//如果会被击倒则不做硬直判定
            {
                role.nowConversely = unit.STAND_CONVESLY_TIME;
                role.changeState(CONVERSELY_NO);
                role.anima.ConverselyStart();
            }
            else if (role.canBeStiff)
            {
                Debug.Log("enter stiff");
                role.nowStiff = damage.stiffTime;
                role.changeState(STIFF_NO);
                role.anima.StiffStart();
            }

        }
        private void treat(int num)
        {
            if (role.nowHp + num > role.maxHp)
            {
                role.nowHp = role.maxHp;
            }
            else
            {
                role.nowHp += num;
            }
        }
    }
    class stiff : state
    {
        private RoleState role;
        public stiff(RoleState role)
        {
            this.role = role;
        }

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

        public int StateNo
        {
            get
            {
                return STIFF_NO;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            if (role.canBetreat)
            {
                if (role.control.On_Been_Treat != null)
                {
                    role.control.On_Been_Treat(RoleState.CreateTreatArg(num,from));
                }
                treat(num);
                role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
            }
        }

        private void hurt(damage damage)
        {
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else if (role.canBeKill)
            {
                role.nowHp = 0;
                role.changeState(DEAD_NO);
                role.anima.ConverselyStart();
                return;
            }
            if (damage.makeConversaly && role.canBeConvesly)//如果会被击倒则不做硬直判定
            {
                role.nowConversely = unit.STAND_CONVESLY_TIME;
                role.changeState(CONVERSELY_NO);
                role.anima.ConverselyStart();
            }
            else if (damage.stiffTime > role.nowStiff)
            {
                role.nowStiff = damage.stiffTime;
            }
        }
        private void treat(int num)
        {
            if (role.nowHp + num > role.maxHp)
            {
                role.nowHp = role.maxHp;
            }
            else
            {
                role.nowHp += num;
            }
        }
        public void onUpdate()
        {
            if (role.nowStiff <= 0)
            {
                role.changeState(0);
                role.anima.StiffEnd();
            }
            else
            {
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
                    
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                  
                }
            }
            if (role.control.On_Take_Damage != null)
            {
                role.control.On_Take_Damage(RoleState.CreateTakeDamageArg(role, damage));
            }
            if (role.control.On_Cause_Damage != null)
            {
                role.control.On_Cause_Damage(RoleState.CreateCauseDamageArg(role, damage));
            }
            hurt(damage);
            role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
        }
    }
    class conversely : state
    {
        private RoleState role;
        public conversely(RoleState role)
        {
            this.role = role;
        }

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

        public int StateNo
        {
            get
            {
                return CONVERSELY_NO;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            if (role.control.On_Been_Treat != null)
            {
                role.control.On_Been_Treat(RoleState.CreateTreatArg(num, from));
            }
            treat(num);
            role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
        }

        public void hurt(damage damage)
        {
            if (role.nowHp - damage.num > 0)
            {
                role.nowHp -= damage.num;
            }
            else if (role.canBeKill)
            {
                role.nowHp = 0;
                role.changeState(DEAD_NO);
                return;
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
                    }
                }
                else if (damage.kind == 2)
                {
                    if (!role.immune_skill)
                    {
                        damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                        damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    }
                }
                if (role.control.On_Take_Damage != null)
                {
                    role.control.On_Take_Damage(RoleState.CreateTakeDamageArg(role, damage));
                }
                if (role.control.On_Cause_Damage != null)
                {
                    role.control.On_Cause_Damage(RoleState.CreateCauseDamageArg(role, damage));
                }
                hurt(damage);
                role.control.On_Hp_Change(RoleState.CreateChangeArg(role));
            }
        }

        public void treat(int num)
        {
            if (role.nowHp + num > role.maxHp)
            {
                role.nowHp = role.maxHp;
            }
            else
            {
                role.nowHp += num;
            }
        }
    }
    class died : state
    {
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
                //Debug.Log("in died can't move");
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

        public int StateNo
        {
            get
            {
                return DEAD_NO;
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

        public void treat(int num)
        {
            return;
        }
    }
    public unit selfdata;
    public Controler control;
    public AnimatorTable anima;
    protected state nowState;
    protected List<state> StateTable=new List<state>();
    public sbyte team;
    //属性
    public int Power
    {
        set
        {
            selfdata.power = value;
        }
        get
        {
            return selfdata.power;
        }
    }
    
    public int Skill
    {
        set
        {
            selfdata.skill = value;
        }
        get
        {
            return selfdata.skill;
        }
    }
    public int Physique
    {
        set
        {
            int newMax = value * unit.STAND_HP;
            if (newMax > maxHp)//临时增加的体质回血
            {
                nowHp += newMax - maxHp;
            }
            else//如果新的最大血量小于现在血量,则强制现在血量变为新最大血量
            {
                if (newMax < nowHp)
                {
                    nowHp = newMax;
                }
            }
            maxHp = newMax;
            selfdata.physique = value;
        }
        get
        {
            return selfdata.physique;
        }
    }
    public int EnergyRecover
    {
        set
        {
            selfdata.energyRecover = value;
        }
        get
        {
            return selfdata.energyRecover;
        }
    }
    public int Accelerate
    {
        set
        {
            selfdata.accelerate = value;
        }
        get
        {
            return selfdata.accelerate;
        }
    }
    private float speedscale = 1;
    public float SpeedScale//速度的倍率
    {
        set
        {
            speedscale = value;
            realspeed = (unit.STAND_SPEED + unit.STAND_SPEED * (((float)selfdata.accelerate) / 100)) * speedscale;
        }
        get
        {
            return speedscale;
        }
    }
    private float realspeed=unit.STAND_SPEED;
    public float RealSpeed//contorler使用的角色真正速度
    {
        get
        {
            return realspeed;
        }
    }
    //隐藏属性-------------------------------------------------
    public int Stiffable{
        set {
            selfdata.stiffable=value;
        }
        get
        {
            return selfdata.stiffable;
        }
    }
    public int stiffReduce//硬直減免
    {
        set
        {
            selfdata.stiffReduce = value;
        }
        get
        {
            return selfdata.stiffReduce;
        }
    }
    public int rechargeLifeRecover{
        set
        {
            selfdata.rechargeEnergyRecover = value;
        }
        get
        {
            return selfdata.rechargeEnergyRecover;
        }
    }
    public int rechargeEnergyRecover {//道具能量恢復加成
        set
        {
            selfdata.rechargeEnergyRecover=value;
        }
        get
        {
            return selfdata.rechargeEnergyRecover;
        }
    }
    //public int attackSpeed;//攻擊速度加成
    public int damageReduce;//物理減傷比例
    public int specialReduce;//技能減傷比例
                             //真實屬性------------------------------------------------------
    public int maxHp;
    
    public int nowHp;
    public float nowMp {
        set
        {
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["nowMp"] = (float)value;
           // Debug.Log("controler is:" + control);
            control.On_MP_Change(Arg);
            mpnum = value;
        }
        get
        {
            return mpnum;
        }
    }
    private float mpnum;
    public float nowStiff;//当前硬直时间
    public float nowConversely;//当前倒地时间
    public float energyRecover_Second;
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
            //Debug.Log("nowState is" + nowState+"lengh"+ StateTable.Count+"  "+hasStart);
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
            Debug.Log(canActionCount + "canactioncount set start");
            if (value)
                canActionCount++;
            else
                canActionCount--;
            Debug.Log(canActionCount + "canactioncount set");
        }
        get
        {
            return nowState.canAction&& canActionCount>0;
            Debug.Log(canActionCount+"canactioncount");
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
  

        StateTable.Add(new normal(this));
        StateTable.Add(new stiff(this));
        StateTable.Add(new conversely(this));
        StateTable.Add(new died());
        nowState = StateTable[0];
        hasStart = true;
        Debug.Log("in start nowState is" + nowState);
        //attackInterval -= attackInterval * (((float)attackSpeed)/100);

    }
    virtual protected void Update()
    {
        nowState.onUpdate();
    }
    public void changeState(int no)
    {
        nowState = StateTable[no];
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
    public static Dictionary<string,object> CreateTakeDamageArg(RoleState role,damage damage)
    {
        Dictionary<string, object> Arg = new Dictionary<string, object>();
        Arg["PlayerPosition"] = role.transform.position;
        Arg["PlayerEuler"] = role.transform.eulerAngles;
        Arg["DamagerPosition"] = damage.damager.transform.position;
        Arg["Damager"] = damage.damager;
        Arg["randomPoint"] = UnityEngine.Random.Range(0, 100);
        Arg["Damage"] = damage;
        return Arg;
    }
    public static Dictionary<string, object> CreateCauseDamageArg(RoleState role, damage damage)
    {
        Dictionary<string, object> Arg = new Dictionary<string, object>();
        Arg["Damage"] = damage;
        Arg["PlayerPosition"] = damage.damager.transform.position;
        Arg["TragetPosition"] = role.transform.position;
        Arg["randomPoint"] = UnityEngine.Random.Range(0, 100);
        Arg["Traget"] = role.gameObject;
        return Arg;
    }
    public static Dictionary<string, object> CreateTreatArg(int num,GameObject from)
    {
        Dictionary<string, object> Arg = new Dictionary<string, object>();
        Arg["Treater"] = from;
        Arg["Num"] = num;
        Arg["randomPoint"] = UnityEngine.Random.Range(0, 100);
        return Arg;
    }
    public static Dictionary<string, object> CreateChangeArg(RoleState state)
    {
        Dictionary<string, object> changeArg = new Dictionary<string, object>();
        float parcent = ((float)state.nowHp) / ((float)state.maxHp);
        changeArg["Percent"] = parcent;
        changeArg["NowHp"] = state.nowHp;
        return changeArg;
    }
}
