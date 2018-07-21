using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingControler : MonoBehaviour,Controler {
    _on_trigger on_take_damage;
    _on_trigger on_inteval;
    _on_trigger on_been_treat;
    _on_trigger on_Hp_change;
    _on_trigger on_cause_damage;
    _on_trigger on_mp_change;
    _on_trigger on_active_skill;
    _on_trigger after_take_damage;
    _on_trigger be_interrupt;

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

    private AnimatorTable action;
    private EquipmentList eList;
    public RoleState state;
    public BuffControler buffcontrol;
    public EquipmentTable table;
    private controtionTable controtions;

    private Dictionary<string, KeyCode> keySetting;
    private int index;
    private List<sbyte> limit;
    //移动动画用变数
    public bool upIng = false;
    public bool leftIng = false;
    public bool downIng = false;
    public bool rightIng = false;
    //恢复mp用变数
    private float nextRecover = unit.RECOVER_MP_INTERVAL;


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
            return false;
        }
        else
        {
            if (eList.nowHarness.passiveEquipments[eindex].CanUse && limit != null && !limit.Contains(eindex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    void onSkillKeyDown(Vector3 mousePos, sbyte KeyCode)
    {
        Debug.Log("enter key down keyCode:" + KeyCode);
        if (eList.passiveEquipments.Count > KeyCode)
        {
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
                     
                    }
                }

                    Dictionary<string, object> args = new Dictionary<string, object>();
                    args["PlayerPosition"] = transform.position;
                    args["MousePosition"] = mousePos;
                    args["randomPoint"] = UnityEngine.Random.Range(0,100);
                    state.nowMp -= ((CDEquipment)eList.equipments[KeyCode]).Consumption;
                    eList.equipments[KeyCode].trigger(args);
                if (on_active_skill != null)
                {
                    on_active_skill(args);
                }
            }
        }
    }
    private Vector3 getmousePos()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = 14;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = 0;
        return mouse;
    }
    // Use this for initialization
    void Start () {
        GameObject temp = GameObject.Find("keyTabel");
        keySetting = temp.GetComponent<KeyRegister>().keySetting;
        eList = GetComponent<EquipmentList>();
        action = GetComponent<AnimatorTable>();
        buffcontrol = GetComponent<BuffControler>();
        table = temp.GetComponent<EquipmentTable>();
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
        on_right_down += onSkillKeyDown;
        on_key1_down += onSkillKeyDown;//因為道具一定是順著順序排放鍵位的,例如第一個道具一定是key1,所以只要判斷主動道具數量就知道角色那個鍵位有沒有主動道具
        on_key2_down += onSkillKeyDown;
        on_key3_down += onSkillKeyDown;
        on_key4_down += onSkillKeyDown;
        on_key5_down += onSkillKeyDown;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = getmousePos();
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
        if (state.canAction)
        {
            if (limit == null || limit.Contains(EquipmentList.PASSIVE1))
            {
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
            if (state.canRota)
            {
                //Debug.Log("canRota is" + state.canRota);
                transform.up = -(mousePos - transform.position);
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
        eList.allReduceCD(Time.deltaTime);
        nextRecover -= Time.deltaTime;
        if (nextRecover <= 0)
        {
            state.recoverMP(Attribute.GetMpRecover(unit.STAND_MP_RECOVER, state.EnergyRecover));
            nextRecover = unit.RECOVER_MP_INTERVAL;
        }
    }


    public void addBuffByNo(sbyte no)
    {

        buffcontrol.AddBuff(table.buffNameList[no]);
    }

    public void distortionByNo(sbyte no)
    {
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
            state.nowNo = no;


            //string typeName = controtions.dataNames[no];
        }
    }
}
