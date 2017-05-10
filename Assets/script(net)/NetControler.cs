using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;

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
    private bool upIng = false;
    private bool leftIng = false;
    private bool downIng = false;
    private bool rightIng = false;
    private AnimatorTable action;
    private EquipmentList eList;
    private List<eTrigger> eTriggerLine=new List<eTrigger>();

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
        eList = GetComponent<EquipmentList>();
        action = GetComponent<AnimatorTable>();
        codeLine = new List<Dictionary<string,object>>();
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
        if (entity != null)
        {
            transform.position = entity.position;
           
        }
        while(codeLine.Count > 0)
        {
            Debug.Log("codeline do------");
            Dictionary<string,object> item = codeLine[0];
            sbyte nextcode = (sbyte)item["code"];
            switch (nextcode)
            {
                case CodeTable.KEYUP_DOWN:
                    {
                        get_on_keyup_down()();
                        break;
                    }
                case CodeTable.KEYLEFT_DOWN:
                    {
                        get_on_keyleft_down()();
                        break;
                    }
                case CodeTable.KEYRIGHT_DOWN:
                    {
                        get_on_keyright_down()();
                        break;
                    }
                case CodeTable.KEYDOWN_DOWN:
                    {
                        get_on_keydown_down()();
                        break;
                    }
                case CodeTable.KEYUP_UP:
                    {
                        get_on_keyup_up()();
                        break;
                    }
                case CodeTable.KEYLEFT_UP:
                    {
                        get_on_keyleft_up()();
                        break;
                    }
                case CodeTable.KEYRIGHT_UP:
                    {
                        get_on_keyright_up()();
                        break;
                    }
                case CodeTable.KEYDOWN_UP:
                    {
                        get_on_keydown_up()();
                        break;
                    }
                case CodeTable.MOUSE_LEFT_DOWN:
                    {
                        get_on_left_down()((Vector3)item["directionZ"]);
                        break;
                    }
            }
            codeLine.RemoveAt(0);

        }
        while (eTriggerLine.Count > 0)
        {
            eTrigger temp = eTriggerLine[0];
            eList.equipments[temp.eIndex].trigger(temp.Args);
            eTriggerLine.RemoveAt(0);
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
        Debug.Log("add order for" + entity.id);
        codeLine.Add(item);
    }

    public void addTriggerOrder(sbyte eIndex, Dictionary<string, object> args)
    {
        eTriggerLine.Add(new eTrigger(eIndex, args));
    }
}
