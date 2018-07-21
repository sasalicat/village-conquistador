using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fsyn_ObsState : RoleState,Controler {
    public const float IMMUNE_TIME = 1;
    public sbyte roomNo;
    public KBControler control;
    public bool islocal = false;
    public float immuneTime = 0;
    public SpriteRenderer render;
    public Color nowcolor = new Color(1, 1, 1, 1);
    public readonly Color WRITER = new Color(1, 1, 1, 1);
    interface state_net : state
    {
        void hurt(damage damage);
        void treat(int num);
    }
    public void empty(Dictionary<string, object> useless)
    {

    }
    class normal : state_net
    {

        private fsyn_ObsState role;
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

        public bool canAction
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

        public normal(fsyn_ObsState role)
        {
            this.role = role;
        }
        public void beenTreat(int num, GameObject from)
        {
            if (role.canBetreat)
            {
                treat(num);
            }
        }

        public void takedamage(damage damage)
        {
            Debug.Log("role takedamage role kind is" + damage.kind + "causer" + damage.damager);
            if (damage.kind == 1)
            {
                if (!role.immune_attack)
                {
                    damage.num = Attribute.ReduceAttackDamageNum(damage.num, role.selfdata.damageReduce);
                    damage.stiffTime = Attribute.ReduceStiff(damage.stiffTime, role.selfdata.stiffReduce);
                    hurt(damage);
                    Debug.Log("send backage");
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num = Attribute.ReduceSpecialDamageNum(damage.num, role.selfdata.damageReduce);
                    damage.stiffTime = Attribute.ReduceStiff(damage.stiffTime, role.selfdata.stiffReduce);
                    hurt(damage);
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

        public void onUpdate()
        {
            bool before = role.immuneTime > 0;
            role.immuneTime -= Time.deltaTime;
            if (before && role.immuneTime <= 0)
            {
                role.immune_attack = false;
                role.immune_skill = false;
                role.render.color = role.WRITER;
            }
            else if (role.immuneTime > 0)//闪烁特效
            {
                role.nowcolor.a = role.immuneTime % 0.3f;
                role.render.color = role.nowcolor;
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
    class stiff : state_net
    {
        private fsyn_ObsState role;
        public stiff(fsyn_ObsState role)
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
                treat(num);
            }
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
            bool before = role.immuneTime > 0;
            role.immuneTime -= Time.deltaTime;
            if (before && role.immuneTime <= 0)
            {
                role.immune_attack = false;
                role.immune_skill = false;
                role.render.color = role.WRITER;
            }
            else if (role.immuneTime > 0)//闪烁特效
            {
                role.nowcolor.a = role.immuneTime % 0.3f;
                role.render.color = role.nowcolor;
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
                   hurt(damage);
                    Debug.Log("send backage");
                }
            }
            else if (damage.kind == 2)
            {
                if (!role.immune_skill)
                {
                    damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                    damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                    hurt(damage);
                    Debug.Log("send backage");
                }
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
    class conversely : state_net
    {
        private fsyn_ObsState role;
        public conversely(fsyn_ObsState role)
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
            if (role.canBetreat)
            {
                treat( num);
            }
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
            if (role.nowConversely <= 0)//切回正常状态
            {
                role.changeState(RoleState.NORMAL_NO);
                role.immuneTime = IMMUNE_TIME;
                role.anima.ConverselyEnd();
                role.immune_attack = true;
                role.immune_skill = true;
            }
            else
            {
                role.nowConversely -= Time.deltaTime;
            }
            bool before = role.immuneTime > 0;
            role.immuneTime -= Time.deltaTime;
            if (before && role.immuneTime <= 0)
            {
                role.immune_attack = false;
                role.immune_skill = false;
                role.render.color = role.WRITER;
            }
            else if (role.immuneTime > 0)//闪烁特效
            {
                role.nowcolor.a = role.immuneTime % 0.3f;
                role.render.color = role.nowcolor;
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
                        hurt(damage);
                        Debug.Log("send backage");
                    }
                }
                else if (damage.kind == 2)
                {
                    if (!role.immune_skill)
                    {
                        damage.num -= (int)(damage.num * (((float)role.selfdata.specialReduce) / 100));
                        damage.stiffTime -= (int)(damage.stiffTime * (((float)role.selfdata.stiffReduce) / 100));
                        hurt(damage);
                        Debug.Log("send backage");
                    }
                }

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
    class died : state_net
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

    protected override void Start()
    {
        base.Start();
        StateTable = new List<state>();
        //重置父類別的StateTable
        StateTable.Add(new normal(this));
        StateTable.Add(new stiff(this));
        StateTable.Add(new conversely(this));
        StateTable.Add(new died());

        control = GetComponent<KBControler>();
        //初始为normal
        nowState = StateTable[0];
        render = GetComponent<SpriteRenderer>();
        //TakeDamage(new damage(1, 100, 1, true, false, gameObject));
        //Debug.Log("getComp:" + GetComponent<RoleState>());
    }

    // Update is called once per frame
    void Update()
    {
        nowState.onUpdate();
    }
    public override void TakeDamage(damage damage)
    {
        Debug.Log("net role state:" + gameObject.name);
        nowState.takedamage(damage);
    }
    public void realHurt(damage damage)
    {
        ((state_net)nowState).hurt(damage);
    }
    public void realTreat(int num)
    {
        ((state_net)nowState).treat(num);
    }
    //假装我是个Controler---------------------------------------------------
    public _on_trigger On_Take_Damage
    {
        set;
        get;
    }
    public _on_trigger On_Interval
    {
        set;
        get;
    }
    public _on_trigger On_Been_Treat
    {
        set;
        get;
    }
    public _on_trigger On_Hp_Change
    {
        set;
        get;
    }
    public _on_trigger On_MP_Change
    {
        set { }
        get { return empty; }
    }
    public _on_trigger On_Cause_Damage
    {
        set;
        get;
    }
    public int Index
    {
        set;
        get;
    }
    public List<sbyte> skillLimit//里面放的是可以且仅可以使用的技能编号,如果不为空则只能使用list里面编号的技能,若为空则没有限制
    {
        get;
        set;
    }

    public _on_trigger On_Active_Skill
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public _on_trigger After_take_damage
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    public _on_trigger Be_Interrupt
    {
        get
        {
            throw new NotImplementedException();
        }

        set
        {
            throw new NotImplementedException();
        }
    }
    void addBuffByNo(sbyte no)
    {

    }
    void distortionByNo(sbyte no)
    {

    }

    public _on_skill_key_down get_on_left_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_right_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_middle_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_key1_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_key2_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_key3_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_key4_down()
    {
        throw new NotImplementedException();
    }

    public _on_skill_key_down get_on_key5_down()
    {
        throw new NotImplementedException();
    }

    public _on_keyup_down get_on_keyup_down()
    {
        throw new NotImplementedException();
    }

    public _on_keyup_ing get_on_keyup_ing()
    {
        throw new NotImplementedException();
    }

    public _on_keyup_up get_on_keyup_up()
    {
        throw new NotImplementedException();
    }

    public _on_keyright_down get_on_keyright_down()
    {
        throw new NotImplementedException();
    }

    public _on_keyright_ing get_on_keyright_ing()
    {
        throw new NotImplementedException();
    }

    public _on_keyright_up get_on_keyright_up()
    {
        throw new NotImplementedException();
    }

    public _on_keydown_down get_on_keydown_down()
    {
        throw new NotImplementedException();
    }

    public _on_keydown_ing get_on_keydown_ing()
    {
        throw new NotImplementedException();
    }

    public _on_keydown_up get_on_keydown_up()
    {
        throw new NotImplementedException();
    }

    public _on_keyleft_down get_on_keyleft_down()
    {
        throw new NotImplementedException();
    }

    public _on_keyleft_ing get_on_keyleft_ing()
    {
        throw new NotImplementedException();
    }

    public _on_keyleft_up get_on_keyleft_up()
    {
        throw new NotImplementedException();
    }

    void Controler.addBuffByNo(sbyte no)
    {
        throw new NotImplementedException();
    }

    void Controler.distortionByNo(sbyte no)
    {
        throw new NotImplementedException();
    }
    public bool equipmentReady(sbyte eindex)
    {
        return false;
    }
}
