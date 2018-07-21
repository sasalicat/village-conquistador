using System;
using System.Collections;
using System.Collections.Generic;
using KBEngine;
using UnityEngine;

public class fsynControler : MonoBehaviour, KBControler
{
    private int index;
    public List<sbyte> limit;
    private bool alive = true;
    private EquipmentTable eTable;
    private PrabTabel prabTable;
    private EquipmentList eList;
    private float nextrecover = unit.RECOVER_MP_INTERVAL;
    private NetRoleState state;
    private System.Random random;
    private AnimatorTable anim;
    private bool lefting=false;
    private bool uping = false;
    private bool righting = false;
    private bool downing = false;
    _on_skill_key_down on_left_down;
    _on_skill_key_down on_right_down;
    _on_skill_key_down on_middle_down;
    _on_skill_key_down on_key1_down;
    _on_skill_key_down on_key2_down;
    _on_skill_key_down on_key3_down;
    _on_skill_key_down on_key4_down;
    _on_skill_key_down on_key5_down;

    _on_keyup_down on_keyup_down;
    _on_keyup_ing on_keyup_ing;
    _on_keyup_up on_keyup_up;
    _on_keyleft_down on_keyleft_down;
    _on_keyleft_ing on_keyleft_ing;
    _on_keyleft_up on_keyleft_up;
    _on_keydown_down on_keydown_down;
    _on_keydown_ing on_keydown_ing;
    _on_keydown_up on_keydown_up;
    _on_keyright_down on_keyright_down;
    _on_keyright_ing on_keyright_ing;
    _on_keyright_up on_keyright_up;

    _on_trigger on_take_damage;
    _on_trigger on_inteval;
    _on_trigger on_been_treat;
    _on_trigger on_Hp_change;
    _on_trigger on_cause_damage;
    _on_trigger on_mp_change;
    _on_trigger on_active_skill;
    _on_trigger after_take_damage;
    _on_trigger be_interrupt;

    public _on_trigger On_Take_Damage
    {
        set
        {
            on_take_damage = value;
        }
        get
        {
            return on_take_damage;
        }
    }
    public _on_trigger On_Interval
    {
        get
        {
            return on_inteval;
        }

        set
        {
            on_inteval = value;
        }
    }

    public _on_trigger On_Been_Treat
    {
        get
        {
            return null;
        }

        set
        {
            on_been_treat = value;
        }
    }

    public _on_trigger On_Hp_Change
    {
        get
        {
            return on_Hp_change;
        }
        set
        {
            on_Hp_change = value;
        }
    }

    public _on_trigger On_Cause_Damage
    {
        get
        {
            return on_cause_damage;
        }

        set
        {
            on_cause_damage = value;
        }
    }

    public _on_trigger On_MP_Change
    {
        get
        {
            return on_mp_change;
        }

        set
        {
            on_mp_change = value;
        }
    }


    public int Index
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
    public List<sbyte> skillLimit
    {
        set
        {
            limit = value;
        }
    }

    public _on_trigger On_Active_Skill
    {
        get
        {
            return on_active_skill;
        }

        set
        {
            on_active_skill = value;
        }
    }

    public _on_trigger After_take_damage
    {
        get
        {
            return after_take_damage;
        }

        set
        {
            after_take_damage = value;
        }
    }
    public _on_trigger Be_Interrupt
    {
        get
        {
            return be_interrupt;
        }

        set
        {
            be_interrupt = value;
        }
    }
    public Entity Entity
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

    public bool Alive//表示角色活着
    {
        get
        {
            return alive;
        }
    }
    public _on_skill_key_down get_on_key1_down()
    {
        return on_key1_down;
    }
    public _on_skill_key_down get_on_key2_down()
    {
        return on_key2_down;
    }
    public _on_skill_key_down get_on_key3_down()
    {
        return on_key3_down;
    }
    public _on_skill_key_down get_on_key4_down()
    {
        return on_key4_down;
    }
    public _on_skill_key_down get_on_key5_down()
    {
        return on_key5_down;
    }

    public _on_keydown_down get_on_keydown_down()
    {
        return on_keydown_down;
    }
    public _on_keydown_ing get_on_keydown_ing()
    {
        return on_keydown_ing;
    }
    public _on_keydown_up get_on_keydown_up()
    {
        return on_keydown_up;
    }
    public _on_keyleft_down get_on_keyleft_down()
    {
        return on_keyleft_down;
    }
    public _on_keyleft_ing get_on_keyleft_ing()
    {
        return on_keyleft_ing;
    }
    public _on_keyleft_up get_on_keyleft_up()
    {
        return on_keyleft_up;
    }
    public _on_keyright_down get_on_keyright_down()
    {
        return on_keyright_down;
    }
    public _on_keyright_ing get_on_keyright_ing()
    {
        return on_keyright_ing;
    }
    public _on_keyright_up get_on_keyright_up()
    {
        return on_keyright_up;
    }
    public _on_keyup_down get_on_keyup_down()
    {
        return on_keyup_down;
    }
    public _on_keyup_ing get_on_keyup_ing()
    {
        return on_keyup_ing;
    }
    public _on_keyup_up get_on_keyup_up()
    {
        return on_keyup_up;
    }

    public _on_skill_key_down get_on_left_down()
    {
        return on_left_down;
    }
    public _on_skill_key_down get_on_middle_down()
    {
        return on_middle_down;
    }
    public _on_skill_key_down get_on_right_down()
    {
        return on_right_down;
    }
    public bool equipmentReady(sbyte eindex)
    {
        if (eList.nowHarness.passiveEquipments.Count <= eindex)
        {
            //Debug.Log("装备准备好回传1:"+ eList.nowHarness.passiveEquipments.Count);
            return false;
        }
        else
        {
            if (!eList.nowHarness.passiveEquipments[eindex].CanUse)
            {
                //Debug.Log("装备准备好回传2:");
                return false;
            }
            else if(limit != null && limit.Contains(eindex))
            {
                //Debug.Log("装备准备好回传3:");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    private void HpChangeHappen()
    {
        Dictionary<string, object> changeArg = new Dictionary<string, object>();
        float parcent = ((float)state.nowHp) / ((float)state.maxHp);
        changeArg["Percent"] = parcent;
        changeArg["NowHp"] = state.nowHp;
        on_Hp_change(changeArg);
    }
    // Use this for initialization
    void Start()
    {
        GameObject table = GameObject.Find("keyTabel");
        eTable = table.GetComponent<EquipmentTable>();
        prabTable = table.GetComponent<PrabTabel>();
        eList = GetComponent<EquipmentList>();
        state = GetComponent<NetRoleState>();
        random = new System.Random();
        anim = GetComponent<AnimatorTable>();
        on_keydown_down += keydown_down;
        on_keydown_up += keydown_up;
        on_keyleft_down += keyleft_down;
        on_keyleft_up += keyleft_up;
        on_keyright_down += keyright_down;
        on_keyright_up += keyright_up;
        on_keyup_down += keyup_down;
        on_keyup_up += keyup_up;
    }
    /*這裡不需要因為synManager在做這些事*/
    public void addOrder(Dictionary<string, object> item)
    {
        Debug.Log("因為想用netroleState");
    }

    public void addTriggerOrder(sbyte eIndex, Dictionary<string, object> args)
    {
        Debug.Log("又不想改框架");
    }

    public void addEvent(sbyte code, Dictionary<string, object> args)
    {
        Debug.Log("所以才繼承KBControler");
    }

    public void Role_onTakeDamage(damage damage)
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = CodeTable.TAKE_DAMAGE;
        order["Damage"] = damage;
        if (damage.damager != null)
            order["DamagerPosition"] = damage.damager.transform.position;
        else
            order["DamagerPosition"] = null;
        order["PlayerPosition"] = transform.position;
        order["randomPoint"] = random.Next(0, 99);
        ((Sender)PostOffice.main).addOrder(order);
    }

    public void Role_onBeenTreat(GameObject treater, int num)
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = CodeTable.BEEN_TREAT;
        order["Treater"] = treater;
        order["Num"] = num;
        order["randomPoint"] = random.Next(0,99);
        ((Sender)PostOffice.main).addOrder(order);
    }

    public void onAttack()
    {
        throw new NotImplementedException();
    }

    public void synchroPos()
    {
        throw new NotImplementedException();
    }


    public void addBuffByNo(sbyte no)
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = CodeTable.ADD_BUFF;
        order["buffNo"] = no;
        ((Sender)PostOffice.main).addOrder(order);
    }

    public void distortionByNo(sbyte no)
    {
        Dictionary<string, object> order = new Dictionary<string, object>();
        order["code"] = CodeTable.CONTORTION;
        order["distortionNo"] = no;
        ((Sender)PostOffice.main).addOrder(order);
    }
    public void takeInterval(Dictionary<string, object> Args)
    {

        //Debug.Log("inveral " + EventLine[0].Args["interval"]);
        if (on_inteval != null)
        {
            on_inteval(Args);
        }
        eList.allReduceCD((float)Args["interval"]);
        nextrecover -= (float)Args["interval"];
        if (nextrecover <= 0)
        {
            state.recoverMP(Attribute.GetMpRecover(unit.STAND_MP_RECOVER, state.EnergyRecover));
            nextrecover = unit.RECOVER_MP_INTERVAL;
        }

    }
    public void setDirection(Vector2 mousePos)
    {
        if(state.canRota)
            transform.up=-( mousePos - (Vector2)transform.position);
    }
    public void move(float interval)
    {
        if (state.canMove)
        {
            Vector2 direction = Vector2.zero;
            if (lefting)
            {
                direction.x += 1;
            }
            if (uping)
            {
                direction.y += 1;
            }
            if (righting)
            {
                direction.x -= 1;
            }
            if (downing)
            {
                direction.y -= 1;
            }
            transform.position = ((Vector2)transform.position + direction.normalized * interval * state.RealSpeed);
        }
    }
    public void realTakeDamage(Dictionary<string,object> Args)
    {
        damage damage = (damage)Args["Damage"];
        if (on_take_damage != null)
        {
            on_take_damage(Args);
        }
        else
        {
            Debug.Log("takedamage null");
        }
        if (damage.damager != null)
        {
            KBControler damageControler = damage.damager.GetComponent<KBControler>();
            if (damageControler.On_Cause_Damage != null)
            {
                Dictionary<string, object> Arg = new Dictionary<string, object>();
                Arg["Damage"] = damage;
                Arg["PlayerPosition"] = Args["DamagerPosition"];
                Arg["TragetPosition"] = Args["PlayerPosition"];
                Arg["randomPoint"] = Args["randomPoint"];
                Arg["Traget"] = this.gameObject;
                //Arg["Traget"]=
                damageControler.On_Cause_Damage(Arg);
                if (damage.equipIndex != -1)
                {
                    Equipment e=   damage.damager.GetComponent<EquipmentList>().equipments[damage.equipIndex];
                    if (e.GetType().GetInterface("canBeAddition")!=null)
                    {
                        if(((canBeAddition)e).onCauseDamage!=null)
                            ((canBeAddition)e).onCauseDamage(Arg);
                    }
                }
            }
        }
        state.realHurt(damage);
        after_take_damage(Args);
        if (damage.stiffTime > 0)
        {
            Be_Interrupt(new Dictionary<string, object>());
        }
        //触发血量变动事件
        HpChangeHappen();
        if (state.nowHp <= 0)
        {
            Dictionary<string, object> diedArg = new Dictionary<string, object>();
            diedArg["Killer"] = (damage).damager;
            alive = false;
            Entity.cellCall("notifyDied", new object[] { (sbyte)0 });

        }
    }
    public void realBeTreat(Dictionary<string,object> Args)
    {
        if (on_been_treat != null)
        {
            on_been_treat(Args);
        }
        state.realTreat((short)Args["Num"]);
        //触发血量变动事件
        HpChangeHappen();
    }
    public void realAddBuff( int buffno)
    {
        GetComponent<BuffControler>().AddBuff(eTable.buffNameList[buffno]);
    }
     public void realControtions( int no)
    {
        var action =GetComponent<AnimatorTable>();
        var eList = GetComponent<EquipmentList>();
        var controtions =  controtionTable.main;
        if (no < 0)
        {
            action.restoreAnimator();
            eList.restoreArmedHarness();
        }
        else
        {
            Debug.Log("收到变身请求");
            RuntimeAnimatorController anim = controtions.animators[no];
            action.controler = anim;
            ContortionData data = controtions.datas[no];
            eList.changeArmedHarness(data);
            controtState state = gameObject.AddComponent<controtState>();
            Debug.Log("Duration:" + data.Duration + "needRecord:" + (data.Duration > 0));
            state.needRecord = data.Duration > 0;
            state.TimeLeft = data.Duration;
            state.nowNo = (sbyte)no;
            Be_Interrupt(new Dictionary<string, object>());

            //string typeName = controtions.dataNames[no];
        }
    }
    private void keyup_down()
    {
        if (!lefting && !righting && !uping && !downing)
        {
            anim.moveStart();
        }
        uping = true;
    }
    private void keyup_up()
    {
        if (!lefting && !righting && uping && !downing)
        {
            anim.moveEnd();
        }
        uping = false;
    }
    private void keyright_down()
    {
        if (!lefting && !righting && !uping && !downing)
        {
            anim.moveStart();
        }
        righting = true;
    }
    private void keyright_up()
    {
        if (!lefting && righting && !uping && !downing)
        {
            anim.moveEnd();
        }
        righting = false;
    }
    private void keydown_down()
    {
        if (!lefting && !righting && !uping && !downing)
        {
            anim.moveStart();
        }
        downing = true;
    }
    private void keydown_up()
    {
        if (!lefting && !righting && !uping && downing)
        {
            anim.moveEnd();
        }
        downing = false;
    }
    private void keyleft_down()
    {
        if (!lefting && !righting && !uping && !downing)
        {
            anim.moveStart();
        }
        lefting = true;
    }
    private void keyleft_up()
    {
        if (lefting && !righting && !uping && !downing)
        {
            anim.moveEnd();
        }
        lefting = false;
    }
    public void onMoveButtom(sbyte action)
    {
        //Debug.Log("onMoveButtom 被呼叫 action:" + action);
        switch (action)
        {
            case CodeTable.KEYLEFT_DOWN:
                {
                    keyleft_down();
                    break;
                }
            case CodeTable.KEYLEFT_UP:
                {
                    keyleft_up();
                    break;
                }
            case CodeTable.KEYUP_DOWN:
                {
                    keyup_down();
                    break;
                }
            case CodeTable.KEYUP_UP:
                {
                    keyup_up();
                    break;
                }
            case CodeTable.KEYRIGHT_DOWN:
                {
                    keyright_down();
                    break;
                }
            case CodeTable.KEYRIGHT_UP:
                {
                    keyright_up();
                    break;
                }
            case CodeTable.KEYDOWN_DOWN:
                {
                    keydown_down();
                    break;
                }
            case CodeTable.KEYDOWN_UP:
                {
                    keydown_up();
                    break;
                }
            
        }
    }

    public void onSkillButtom(sbyte action,Dictionary<string,object> args)
    {
        switch (action)
        {
            case CodeTable.MOUSE_LEFT_DOWN:
                {
                    Debug.Log("武裝帶上的技能數:" + eList.nowHarness.passiveEquipments.Count);
                    eList.nowHarness.passiveEquipments[0].trigger(args);
                    break;
                }
            case CodeTable.MOUSE_RIGHT_DOWN:
                {
                    eList.nowHarness.passiveEquipments[1].trigger(args);
                    break;
                }
            case CodeTable.KEY1_DOWN:
                {
                    eList.nowHarness.passiveEquipments[2].trigger(args);
                    break;
                }
            case CodeTable.KEY2_DOWN:
                {
                    eList.nowHarness.passiveEquipments[3].trigger(args);
                    break;
                }
            case CodeTable.KEY3_DOWN:
                {
                    eList.nowHarness.passiveEquipments[4].trigger(args);
                    break;
                }
            case CodeTable.KEY4_DOWN:
                {
                    eList.nowHarness.passiveEquipments[5].trigger(args);
                    break;
                }
            case CodeTable.KEY5_DOWN:
                {
                    eList.nowHarness.passiveEquipments[6].trigger(args);
                    break;
                }
        }
    }
}