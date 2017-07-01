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
    public Entity entity;
    public List<Dictionary<string, object>> codeLine;
    public bool upIng = false;
    public bool leftIng = false;
    public bool downIng = false;
    public bool rightIng = false;
    private AnimatorTable action;
    private EquipmentList eList;
    private NetRoleState state;
    private List<eTrigger> eTriggerLine=new List<eTrigger>();
    private List<eTrigger> EventLine = new List<eTrigger>();
    public Text Label;
    //装备事件
    _on_attack on_attack;

    _on_left_down on_left_down;
    _on_right_down on_right_down;
    _on_middle_down on_middle_down;
    _on_key1_down on_key1_down;
    _on_key2_down on_key2_down;
    _on_key3_down on_key3_down;
    _on_key4_down on_key4_down;
    _on_key5_down on_key5_down;
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

    _on_take_damage on_take_damage;
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

    public _on_take_damage On_Take_Damage
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

    public _on_key1_down get_on_key1_down()
    {
        return on_key1_down;
    }
    public _on_key2_down get_on_key2_down()
    {
        return on_key2_down;
    }
    public _on_key3_down get_on_key3_down()
    {
        return on_key3_down;
    }
    public _on_key4_down get_on_key4_down()
    {
        return on_key4_down;
    }
    public _on_key5_down get_on_key5_down()
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
    public _on_left_down get_on_left_down()
    {
        return on_left_down;
    }
    public _on_middle_down get_on_middle_down()
    {
        return on_middle_down;
    }
    public _on_right_down get_on_right_down()
    {
        return on_right_down;
    }

    void Start()
    {
        Label = GameObject.Find("Canvas/Text").GetComponent<Text>();
        //Label.text = "我活著";
        eList = GetComponent<EquipmentList>();
        action = GetComponent<AnimatorTable>();
        codeLine = new List<Dictionary<string,object>>();
        state = GetComponent<NetRoleState>();
        //添加控制器事件
        on_keyleft_down += onKeyLeftDown;
        on_keydown_down += onKeyDownDown;
        on_keyright_down += onKeyRightDown;
        on_keyup_down += onKeyUpDown;
        on_keydown_up += onKeyDownUp;
        on_keyup_up += onKeyUpUp;
        on_keyleft_up += onKeyLeftUp;
        on_keyright_up += onKeyRightUp;
        on_left_down += onMouseLeftDown;
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
                changeV3.x += state.speed * Time.deltaTime;
            }
            if (rightIng)
            {
                changeV3.x -= state.speed * Time.deltaTime;
            }
            if (upIng)
            {
                changeV3.y += state.speed * Time.deltaTime;
            }
            if (downIng)
            {
                changeV3.y -= state.speed * Time.deltaTime;
            }
            transform.position += changeV3;
        }
        //-------------------------------------------
        while (eTriggerLine.Count > 0)
        {
            
            //Label.text = "enter etrigger! size:"+ eList.equipments.Count;
            eTrigger temp = eTriggerLine[0];
            //Label.text = "in eTrigger gameobj is"+ eList.equipments[temp.eIndex].ToString();
            eList.equipments[temp.eIndex].trigger(temp.Args);
            Debug.Log("in eTrigger eIndex is"+temp.eIndex);
            eTriggerLine.RemoveAt(0);
           
        }
        while (EventLine.Count > 0)
        {
            switch (EventLine[0].eIndex) {
                case CodeTable.TAKE_DAMAGE:
                    {
                        if (get_on_take_damage() != null)
                        {
                            get_on_take_damage()(EventLine[0].Args);
                        }
                        state.realHurt((damage)EventLine[0].Args["Damage"]);
                        break;
                    }
                case CodeTable.INTERVAL:
                    {
                        Debug.Log("inveral "+ EventLine[0].Args["interval"]);
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

    public _on_attack get_on_attack()
    {
        return on_attack;
    }

    public _on_take_damage get_on_take_damage()
    {
        return on_take_damage;
    }

    public void addEvent(sbyte code, Dictionary<string, object> args)
    {
        EventLine.Add(new eTrigger(code, args));
    }
}
