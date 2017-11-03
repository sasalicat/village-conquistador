using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;


public class NetPlayerControler : MonoBehaviour,KBControler {
    private class eTrigger//裝備觸發指令,eIndex為裝備的Index
    {
        public sbyte eIndex;
        public Dictionary<string, object> Args;
        public eTrigger(sbyte eIndex, Dictionary<string, object> Args)
        {
            this.eIndex = eIndex;
            this.Args = Args;
        }
    }
    public const float UPDATE_Z_INTERVAL = 0.1f;
    public const float RECOVER_INTERVAL = 0.5f;

    public Entity entity;
    public sbyte roomNo;
    private int lastZ=0;
    private float timeInterval = 0;
    private Dictionary<string, KeyCode> keySetting;
    private AnimatorTable action;
    private bool upIng=false;
    private bool leftIng = false;
    private bool downIng= false;
    private bool rightIng = false;
    private Player player;
    private EquipmentList eList;
    private NetRoleState state;
    private BuffControler buffcontrol;
    private controtionTable controtions;
    private string[] buffTable;
    private float nextrecover = 0.5f;
    private RuntimeAnimatorController originAnim;
    private int index;
    private List<sbyte> limit;
    private bool alive = true;

    private List<eTrigger> eTriggerLine = new List<eTrigger>();
    private List<eTrigger> EventLine = new List<eTrigger>();//用於儲存服務器發過來的事件,為了節省腳本長度仍然使用eTrigger,使用eIndex來代表事件編號而非裝備索引

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
    //新架構儲存觸發物件

    List<Equipment> onAttackLine=new List<Equipment>();
    public bool Alive//表示角色活着
    {
        get
        {
            return alive;
        }
    }
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

    private Vector3 getmousePos()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 33;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = 0;
        return mouse;
    }
     void Start()
    {

        GameObject temp = GameObject.Find("keyTabel");
        eList = GetComponent<EquipmentList>();
        keySetting =temp.GetComponent<KeyRegister>().keySetting;
        action = GetComponent<AnimatorTable>();
        player = ((Player)KBEngineApp.app.player());
        state = GetComponent<NetRoleState>();
        buffcontrol = GetComponent<BuffControler>();
        buffTable = temp.GetComponent<EquipmentTable>().buffNameList;
        controtions = temp.GetComponent<controtionTable>();
        //添加控制器事件
        on_keyleft_down += onKeyLeftDown;
        on_keydown_down += onKeyDownDown;
        on_keyright_down += onKeyRightDown;
        on_keyup_down += onKeyUpDown;
        on_keydown_up += onKeyDownUp;
        on_keyup_up += onKeyUpUp;
        on_keyleft_up += onKeyLeftUp;
        on_keyright_up += onKeyRightUp;
       
        on_left_down += onSkillKeyDown;

       /* else
        {
            on_left_down += empty;
        }*/

        on_right_down += onSkillKeyDown;
        /*else
        {
            on_left_down += empty;
        }*/
      
        on_key1_down += onSkillKeyDown;//因為道具一定是順著順序排放鍵位的,例如第一個道具一定是key1,所以只要判斷主動道具數量就知道角色那個鍵位有沒有主動道具

        /*else
        {
            on_key1_down += empty;
        }*/

        on_key2_down += onSkillKeyDown;
        /*else
        {
            on_key2_down += empty;
        }*/
        on_key3_down += onSkillKeyDown;

        /*else
        {
            on_key3_down += empty;
        }*/

       on_key4_down += onSkillKeyDown;

        /*else
        {
            on_key4_down += empty;
        }*/
        on_key5_down += onSkillKeyDown;
        /*else
        {
            on_key5_down += empty;
        }
        on_been_treat += (Dictionary<string, object> Arg) => Debug.Log("触发治疗事件");*/
    }
    void OnEnable()
    {
        transform.position = entity.position;//在setActive时同步角色位置
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
        timeInterval += Time.deltaTime;
        int nowz = (int)transform.eulerAngles.z;
        Vector3 mousePos = getmousePos();
        if (state.canRota) {
            //Debug.Log("canRota is" + state.canRota);
            transform.up = -(mousePos - transform.position);

            if (nowz != lastZ && timeInterval >= UPDATE_Z_INTERVAL)
            {

                short ans = (short)(nowz);
                //Debug.Log("Z change nowz is"+nowz+" ans is" + ans);
                ((Player)entity).cellCall("updateZ", new object[] { ans });

                timeInterval = 0;
                lastZ = nowz;
            }
        }
        if (entity != null)
        {
          
                //Debug.Log("enter move");
                
                //entity.position = transform.position;
                //entity.direction = transform.eulerAngles;
                
                //Debug.Log("up is"+KeyCode.UpArrow);
                Vector3 changeV3 = new Vector3(0, 0, 0);
                if (Input.GetKeyDown(keySetting["up"]))
                {
                    on_keyup_down();
                }
                if (Input.GetKeyDown(keySetting["left"]))
                {
                    on_keyleft_down();
                }
                if (Input.GetKeyDown(keySetting["down"]))
                {
                    on_keydown_down();
                }
                if (Input.GetKeyDown(keySetting["right"]))
                {
                    on_keyright_down();
                }
                if (Input.GetKey(keySetting["up"]))
                {
                    changeV3.y += state.RealSpeed * Time.deltaTime;
                    //on_keyup_ing();
                }
                if (Input.GetKey(keySetting["left"]))
                {
                    changeV3.x += state.RealSpeed * Time.deltaTime;
                    //on_keyleft_ing();
                }
                if (Input.GetKey(keySetting["down"]))
                {
                    changeV3.y -= state.RealSpeed * Time.deltaTime;
                    //on_keydown_ing();
                }
                if (Input.GetKey(keySetting["right"]))
                {
                    changeV3.x -= state.RealSpeed * Time.deltaTime;
                    //on_keyright_ing();
                }
                if (Input.GetKeyUp(keySetting["up"]))
                {

                    on_keyup_up();
                }
                if (Input.GetKeyUp(keySetting["left"]))
                {

                    on_keyleft_up();
                }
                if (Input.GetKeyUp(keySetting["down"]))
                {

                    on_keydown_up();
                }
                if (Input.GetKeyUp(keySetting["right"]))
                {

                    on_keyright_up();
                }
            if (state.canAction)
            {
                if (limit==null||limit.Contains(EquipmentList.PASSIVE1)) {
                    if (Input.GetKeyDown(keySetting["key1"]))
                    {
                        on_key1_down(mousePos, EquipmentList.PASSIVE1);
                    }
                }
                if (limit == null || limit.Contains(EquipmentList.PASSIVE2))
                {
                    if (Input.GetKeyDown(keySetting["key2"]))
                    {
                        on_key2_down(mousePos, EquipmentList.PASSIVE2);
                    }
                }
                if (limit == null || limit.Contains(EquipmentList.PASSIVE3))
                {
                    if (Input.GetKeyDown(keySetting["key3"]))
                    {
                        on_key3_down(mousePos, EquipmentList.PASSIVE3);
                    }
                }
                if (limit == null || limit.Contains(EquipmentList.PASSIVE4))
                {
                    if (Input.GetKeyDown(keySetting["key4"]))
                    {
                        on_key4_down(mousePos, EquipmentList.PASSIVE4);
                    }
                }
                if (limit == null || limit.Contains(EquipmentList.PASSIVE5))
                {
                    if (Input.GetKeyDown(keySetting["key5"]))
                    {
                        on_key5_down(mousePos, EquipmentList.PASSIVE5);
                    }
                }
                //鼠標
                if (limit == null || limit.Contains(EquipmentList.ATK))
                {
                    if (Input.GetMouseButtonDown(0))
                    {

                        //Debug.Log("NetPlayerControler:position" + transform.position + "mouse Position" + mousePos);
                        on_left_down(mousePos, EquipmentList.ATK);
                        //Debug.Log("num" + on_left_down.GetInvocationList().Length + "after mousePos is" + mousePos);
                    }
                }
                if (limit == null || limit.Contains(EquipmentList.SKILL))
                {
                    if (Input.GetMouseButtonDown(1))
                    {

                        //Debug.Log("NetPlayerControler:position" + transform.position + "mouse Position" + mousePos);
                        on_right_down(mousePos, EquipmentList.SKILL);
                        //Debug.Log("num" + on_left_down.GetInvocationList().Length + "after mousePos is" + mousePos);
                    }
                }
            }
            if (state.canMove)
            {
                transform.position += changeV3;
            }

        //處理觸發事件
            while (eTriggerLine.Count > 0)
            {
                eTrigger temp = eTriggerLine[0];
                Debug.Log("netPlayerControler eIndex is"+temp.eIndex);
                state.nowMp -= ((CDEquipment)eList.equipments[temp.eIndex]).Consumption;
                eList.equipments[temp.eIndex].trigger(temp.Args);
              
                eTriggerLine.RemoveAt(0);
            }
            while (EventLine.Count > 0)
            {
                //Debug.Log("code is"+ EventLine[0].eIndex);
                switch (EventLine[0].eIndex)
                {
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
                                state.recoverMP(Attribute.GetMpRecover(unit.STAND_MP_RECOVER, state.EnergyRecover));
                                nextrecover = unit.RECOVER_MP_INTERVAL;
                            }
                            break;
                        }
                    case CodeTable.TAKE_DAMAGE:
                        {
                            Debug.Log("event-takedamage");
                            damage damage = (damage)EventLine[0].Args["Damage"];
                            if (on_take_damage != null)
                            {
                                on_take_damage(EventLine[0].Args);
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
                                    Arg["PlayerPosition"] = EventLine[0].Args["DamagerPosition"];
                                    Arg["TragetPosition"] = EventLine[0].Args["PlayerPosition"];
                                    Arg["randomPoint"] = EventLine[0].Args["randomPoint"];
                                    Arg["Traget"] = this.gameObject;
                                    //Arg["Traget"]=
                                    damageControler.On_Cause_Damage(Arg);
                                }
                            }
                            state.realHurt(damage);
                            //触发血量变动事件
                            HpChangeHappen();
                            if (state.nowHp<=0)
                            {
                                Dictionary<string, object> diedArg = new Dictionary<string, object>();
                                diedArg["Killer"] = (damage).damager;
                                alive = false;
                                Entity.cellCall("notifyDied", new object[] {(sbyte)0});

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
                            //触发血量变动事件
                            HpChangeHappen();
                            break;
                        }
                    case CodeTable.ADD_BUFF:
                        {
                            buffcontrol.AddBuff(buffTable[(sbyte)EventLine[0].Args["buffNo"]]);
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
                                Debug.Log("收到变身请求");
                                RuntimeAnimatorController anim = controtions.animators[no];
                                action.controler = anim;
                                ContortionData data = controtions.datas[no];
                                eList.changeArmedHarness(data);
                                controtState state= gameObject.AddComponent<controtState>();
                                Debug.Log("Duration:" + data.Duration + "needRecord:" + (data.Duration > 0));
                                state.needRecord = data.Duration > 0;
                                state.TimeLeft = data.Duration;
                                state.nowNo = no;

                                
                                //string typeName = controtions.dataNames[no];
                            }
                            break;
                        }
                    case CodeTable.SYNCHRO:
                        {
                            break;
                        }
                }
                EventLine.RemoveAt(0);
            }
        }
        //装备冷却------------------------------------------------------------

    }
    //基本控制的
    void onKeyUpDown()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] {new Vector2(position.x,position.y),CodeTable.KEYUP_DOWN});
        action.moveStart();
        upIng = true;
    }
    void onKeyDownDown()
    {
        //player.baseCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_DOWN });
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYDOWN_DOWN });
        action.moveStart();
        downIng = true;
    }
    void onKeyLeftDown()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYLEFT_DOWN });
        action.moveStart();
        leftIng = true;
    }
    void onKeyRightDown()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYRIGHT_DOWN});
        action.moveStart();
        rightIng = true;
    }
    void onKeyUpUp()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYUP_UP });
        upIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
     }
    void onKeyDownUp()
    {
        //player.baseCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_UP });
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYDOWN_UP });
        downIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyLeftUp()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYLEFT_UP });
        leftIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyRightUp()
    {
        Vector3 position = transform.position;
        player.cellCall("notify1", new object[] { new Vector2(position.x, position.y), CodeTable.KEYRIGHT_UP });
        rightIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }

    void onSkillKeyDown(Vector3 mousePos,sbyte KeyCode)
    {
        Debug.Log("enter key down keyCode:"+KeyCode);
        if (eList.passiveEquipments.Count>KeyCode) {
            if (eList.passiveEquipments[KeyCode].CanUse)
            {
                if (eList.NeedCast[KeyCode])
                {
                    Debug.Log("enter need cast");
                    Debug.DrawRay(mousePos, transform.forward, Color.red, 10);
                    RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, 10);
                    Debug.Log("enter need cast:" + hit);
                    if (hit && (hit.collider.tag == "Player"))
                    {
                        Debug.Log("enter need cast2");
                        sbyte tragetNo = hit.collider.GetComponent<NetRoleState>().roomNo;
                        player.cellCall("notify3_ap", new object[] { eList.passiveEquipments[KeyCode].selfIndex, transform.position, tragetNo, hit.transform.position });
                    }
                }
                else
                {
                    Debug.Log("do not need cast");
                    player.cellCall("notify3", new object[] { eList.passiveEquipments[KeyCode].selfIndex, transform.position, mousePos });
                }
            }
        }
    }
    
    void empty(Vector3 mousePos,sbyte Code)
    {
        return;
    }
    public void addOrder(Dictionary<string, object> item)//移動按鍵操作的動畫,由於用的是位置同步所以本機角色並不需要同步位置
    {//鏡像角色則需要接受同步動畫
        return;
    }

    public void addTriggerOrder(sbyte eIndex, Dictionary<string, object> args)
    {
        Debug.Log("addTrigger in net contrler");
        eTriggerLine.Add(new eTrigger(eIndex, args));
    }

    public void Role_onTakeDamage(damage damage)//这是给RoleState用的
    {
       
        sbyte kind = (sbyte)damage.kind;
        short num = (short)damage.num;
        //处理成为毫秒用short形态传输节省流量:乘以1000后省去小数点后
        short stiffMilli = (short)(damage.stiffTime*1000);
        sbyte damagerNo = -1;
        Vector3 damagerPos = Vector3.zero;
        if (damage.damager != null)
        {
            damagerNo = damage.damager.GetComponent<NetRoleState>().roomNo;
            damagerPos=damage.damager.transform.position;
        }
        

        
        Debug.Log("player:" + player + " damage:" + damage);
        ((Player)KBEngineApp.app.player()).cellCall("notify4", new object[] { transform.position, transform.eulerAngles, damagerPos, damagerNo, kind, num, stiffMilli, damage.makeConversaly, damage.hitConversely });

        //player.cellCall("notify4", new object[] { transform.position,transform.eulerAngles, damagerPos,damagerNo,kind,num,stiffMilli,damage.makeConversaly,damage.hitConversely });
    }

    public void onAttack()
    {
        throw new NotImplementedException();
    }


    public void addEvent(sbyte code, Dictionary<string, object> args)
    {
        //Debug.Log("add event");
        EventLine.Add(new eTrigger(code, args));
    }

    public void Role_onBeenTreat(GameObject treater, int num)
    {//因为只有本机角色能真正治疗,所以对于本机角色来说不可能被别人治疗
        Debug.Log("treater is " + treater);
        if (treater==null)//治療者為null說明為道具治療
        {
            Entity.cellCall("notify5", new object[] { -1, num });
        }
        else if (treater.GetComponent<NetRoleState>().islocal)
        {
            Entity.cellCall("notify5", new object[] { treater.GetComponent<NetRoleState>().roomNo, num });
        }
    }

    public void addBuffByNo(sbyte no)
    {
        KBEngineApp.app.player().cellCall("addBuff",new object[] {no});
    }

    public void distortionByNo(sbyte no)
    {
        if(GetComponent<controtState>()==null)//只有没有被变形才能被变形
            KBEngineApp.app.player().cellCall("distortion", new object[] { no });
    }

    public void synchroPos()
    {
        sbyte buttomState = 0;//目前按键状态的变数
        if (upIng)
        {
            buttomState += 1;
        }
        if (rightIng)
        {
            buttomState += 2;
        }
        if (downIng)
        {
            buttomState += 4;
        }
        if (leftIng)
        {
            buttomState += 8;
        }

        KBEngineApp.app.player().cellCall("synchroPos", new object[] { transform.position,buttomState });
    }


    void OnApplicationFocus(bool hasFocus)
    {
        Debug.Log("hasForce" + hasFocus);
        if (!hasFocus)
        {
            upIng = false;
            rightIng = false;
            downIng = false;
            leftIng = false;
            synchroPos();
        }
    }
}
