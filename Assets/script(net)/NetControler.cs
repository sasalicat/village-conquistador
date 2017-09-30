using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class NetControler : MonoBehaviour,KBControler{
    private class eTrigger
    {
        public sbyte eIndex;
        public Dictionary<string, object> Args;
        public eTrigger(sbyte eIndex,Dictionary<string,object> Args)
        {
            this.eIndex = eIndex;
            this.Args = Args;
        }
    }
    public const float RECOVER_INTERVAL = 0.5f;
    public Entity entity;
    public List<Dictionary<string, object>> codeLine;
    public bool upIng = false;
    public bool leftIng = false;
    public bool downIng = false;
    public bool rightIng = false;
    private AnimatorTable action;
    private EquipmentList eList;
    private NetRoleState state;
    private controtionTable controtions;
    private List<eTrigger> eTriggerLine=new List<eTrigger>();
    private List<eTrigger> EventLine = new List<eTrigger>();
    public BuffControler buffcontrol;
    public Text Label;
    private float nextrecover = 0.5f;
    private string[] buffTable;
    private int index;
    private List<sbyte> limit;
    //装备事件
    _on_trigger on_attack;

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
    public Entity Entity
    {
        get
        {
            return entity;
        }

        set
        {
            entity = value;
        }
    }

    public EquipmentList equipmentList
    {
        get
        {
            return eList;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

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
            return on_been_treat;
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

    void Start()
    {
        //Label = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //Label.text = "我活著";
        eList = GetComponent<EquipmentList>();
        action = GetComponent<AnimatorTable>();
        codeLine = new List<Dictionary<string,object>>();
        state = GetComponent<NetRoleState>();
        buffcontrol = GetComponent<BuffControler>();
        controtions = GameObject.Find("keyTabel").GetComponent<controtionTable>();
        buffTable = GameObject.Find("keyTabel").GetComponent<EquipmentTable>().buffNameList;
        //添加控制器事件
        on_keyleft_down += onKeyLeftDown;
        on_keydown_down += onKeyDownDown;
        on_keyright_down += onKeyRightDown;
        on_keyup_down += onKeyUpDown;
        on_keydown_up += onKeyDownUp;
        on_keyup_up += onKeyUpUp;
        on_keyleft_up += onKeyLeftUp;
        on_keyright_up += onKeyRightUp;

    }
    private void HpChangeHappen()
    {
        Dictionary<string, object> changeArg = new Dictionary<string, object>();
        float parcent = ((float)state.nowHp) / ((float)state.maxHp);
        changeArg["Percent"] = parcent;
        changeArg["NowHp"] = state.nowHp;
        on_Hp_change(changeArg);
    }
    void Update()
    {
        //Label.text = ".........";
        /*if (entity != null)
        {
            transform.position = entity.position;
           
        }*/
        while(codeLine.Count > 0)
        {
            //Debug.Log("codeline do------");
            Dictionary<string,object> item = codeLine[0];
            sbyte nextcode = (sbyte)item["code"];
            // Debug.Log("code " + nextcode);
          
            switch (nextcode)//先同步位置再动画
            {
                case CodeTable.KEYUP_DOWN:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x,Pos.y,0);
                        get_on_keyup_down()();
                        break;
                    }
                case CodeTable.KEYLEFT_DOWN:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keyleft_down()();
                        break;
                    }
                case CodeTable.KEYRIGHT_DOWN:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keyright_down()();
                        break;
                    }
                case CodeTable.KEYDOWN_DOWN:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keydown_down()();
                        break;
                    }
                case CodeTable.KEYUP_UP:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keyup_up()();
                        break;
                    }
                case CodeTable.KEYLEFT_UP:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keyleft_up()();
                        break;
                    }
                case CodeTable.KEYRIGHT_UP:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keyright_up()();
                        break;
                    }
                case CodeTable.KEYDOWN_UP:
                    {
                        Vector2 Pos = (Vector2)item["PlayerPosition"];
                        this.transform.position = new Vector3(Pos.x, Pos.y, 0);
                        get_on_keydown_up()();
                        break;
                    }
                case CodeTable.UPDATE_Z:
                    {
                        if (state.canRota)
                        {
                            //用dotween来平滑转向
                            short directZ = (short)item["Z"];
                            transform.DORotate(new Vector3(0, 0, directZ), 0.1f, RotateMode.Fast);
 
                        }
                        break;
                    }
            }
            codeLine.RemoveAt(0);

        }
        //移动---------------------------------------
        //Vector3 changeV3 = Vector3.zero;
        if (state.canMove)
        {
            Vector3 changeV3 = new Vector3(0, 0, 0);
            if (leftIng)
            {
                changeV3.x += state.RealSpeed * Time.deltaTime;
            }
            if (rightIng)
            {
                changeV3.x -= state.RealSpeed * Time.deltaTime;
            }
            if (upIng)
            {
                changeV3.y += state.RealSpeed * Time.deltaTime;
            }
            if (downIng)
            {
                changeV3.y -= state.RealSpeed * Time.deltaTime;
            }
            transform.position += changeV3;
        }
        //-------------------------------------------
        while (eTriggerLine.Count > 0)
        {
            
            //Label.text = "enter etrigger! size:"+ eList.equipments.Count;
            eTrigger temp = eTriggerLine[0];
            //Label.text = "in eTrigger gameobj is"+ eList.equipments[temp.eIndex].ToString();
            Debug.Log("eindex is" + temp.eIndex);
            eList.equipments[temp.eIndex].trigger(temp.Args);

            eTriggerLine.RemoveAt(0);
           
        }
        while (EventLine.Count > 0)
        {
            switch (EventLine[0].eIndex) {
                case CodeTable.TAKE_DAMAGE:
                    {
                        damage damage = (damage)EventLine[0].Args["Damage"];
                        if (on_take_damage != null)
                        {
                            on_take_damage(EventLine[0].Args);
                        }
                        KBControler damageControler = damage.damager.GetComponent<KBControler>();
                        if (damageControler.On_Cause_Damage != null)
                        {
                            Dictionary<string, object> Arg = new Dictionary<string, object>();
                            Arg["Damage"] = damage;
                            Arg["PlayerPosition"] = EventLine[0].Args["DamagerPosition"];
                            Arg["TragetPosition"] = EventLine[0].Args["PlayerPosition"];
                            Arg["randomPoint"] = EventLine[0].Args["randomPoint"];
                            Arg["Traget"] = this.gameObject;
                            //Arg["Traget"]=
                            damageControler.On_Cause_Damage(Arg);
                        }
                        state.realHurt((damage)EventLine[0].Args["Damage"]);
                        HpChangeHappen();
                        break;
                    }
                case CodeTable.INTERVAL:
                    {
                        //Debug.Log("inveral " + EventLine[0].Args["interval"]);
                        if (on_inteval != null)
                        {
                            on_inteval(EventLine[0].Args);
                        }
                        eList.allReduceCD((float)EventLine[0].Args["interval"]);
                        nextrecover -= (float)EventLine[0].Args["interval"];
                        if (nextrecover <= 0)
                        {
                            state.recoverMP(Attribute.GetMpRecover(unit.STAND_MP_RECOVER,state.EnergyRecover));
                            nextrecover = unit.RECOVER_MP_INTERVAL;
                        }
                        break;
                    }
                case CodeTable.BEEN_TREAT:
                    {
                        if (on_been_treat != null)
                        {
                            on_been_treat(EventLine[0].Args);
                        }
                        state.realTreat((short)EventLine[0].Args["Num"]);
                        HpChangeHappen();
                        break;
                    }
                case CodeTable.ADD_BUFF:
                    {
                        sbyte no = (sbyte)EventLine[0].Args["buffNo"];
                        Debug.Log("buffNo"+no);
                        buffcontrol.AddBuff(buffTable[(sbyte)EventLine[0].Args["buffNo"]]);
                        //buffcontrol.AddBuff();
                        break;
                    }
                case CodeTable.CONTORTION:
                    {

                        sbyte no = (sbyte)EventLine[0].Args["contortionNo"];
                        if (no < 0)
                        {
                            action.restoreAnimator();
                            eList.restoreArmedHarness();
                        }
                        else
                        {
                            RuntimeAnimatorController anim = controtions.animators[no];
                            action.controler = anim;
                            ContortionData data = controtions.datas[no];
                            eList.changeArmedHarness(data);
                            controtState state = gameObject.AddComponent<controtState>();
                            state.needRecord = data.Duration > 0;
                            state.TimeLeft = data.Duration;
                            state.nowNo = no;
                        }
                        //string typeName = controtions.dataNames[no];

                        break;
                    }
                case CodeTable.SYNCHRO:
                    {
                        sbyte buttoms = (sbyte)EventLine[0].Args["buttomState"];
                        Vector3 pos= (Vector3)EventLine[0].Args["position"];
                        beenSynchro(pos, buttoms);
                        break;
                    }
            }
            EventLine.RemoveAt(0);
        }
    }
    void onKeyUpDown()
    {
        action.moveStart();
        upIng = true;
    }
    void onKeyDownDown()
    {
        action.moveStart();
        downIng = true;
    }
    void onKeyLeftDown()
    {
        action.moveStart();
        leftIng = true;
    }
    void onKeyRightDown()
    {
        action.moveStart();
        rightIng = true;
    }
    void onKeyUpUp()
    {
        upIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyDownUp()
    {

        downIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyLeftUp()
    {
        leftIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyRightUp()
    {
        rightIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onMouseLeftDown(Vector3 mousePos)
    {
        transform.up = -(mousePos - transform.position);
        action.AttackStart();
    }

    public void addOrder(Dictionary<string, object> item)
    {
        //Debug.Log("add order for" + entity.id);
        codeLine.Add(item);
    }

    public void addTriggerOrder(sbyte eIndex, Dictionary<string, object> args)
    {
        Debug.Log("netcontroler AddTrigger");
        eTriggerLine.Add(new eTrigger(eIndex, args));
    }

    public void Role_onTakeDamage(damage damage)
    {
        return;
    }

    public void onAttack()
    {
        return;
    }



    public void addEvent(sbyte code, Dictionary<string, object> args)
    {
        EventLine.Add(new eTrigger(code, args));
    }

    public void Role_onBeenTreat(GameObject treater, int num)
    {
        if (treater.GetComponent<NetRoleState>().islocal)
        {
            Entity.cellCall("notify5", new object[] {treater.GetComponent<NetRoleState>().roomNo,num});
        }
    }

    public void addBuffByNo(sbyte no)
    {
        return;
    }

    public void distortionByNo(sbyte no)
    {
        return;
    }

    public void synchroPos()
    {
        return;
    }

    public void beenSynchro(Vector3 pos,sbyte ButtomState)
    {
        Debug.Log("beenSynchro 被呼叫");
        this.transform.position = pos;
        int buttoms = ButtomState;
        if (buttoms / 8 > 0)
        {
            leftIng = true;
        }
        else
        {
            leftIng = false;
        }
        buttoms = buttoms % 8;
        if (buttoms / 4 > 0)
        {
            downIng = true;
        }
        else
        {
            downIng = false;
        }
        buttoms = buttoms % 4;
        if (buttoms / 2 > 0)
        {
            rightIng = true;
        }
        else
        {
            rightIng = false;
        }
        buttoms = buttoms % 2;
        if (buttoms / 1 > 0)
        {
            upIng = true;
        }
        else
        {
            upIng = false;
        }
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
}
