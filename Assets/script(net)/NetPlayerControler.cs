﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;

public class NetPlayerControler : MonoBehaviour,KBControler {
    private class eTrigger
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
    private List<eTrigger> eTriggerLine = new List<eTrigger>();

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
            throw new NotImplementedException();
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

        GameObject temp = GameObject.Find("keyTabel");
        eList = GetComponent<EquipmentList>();
        keySetting =temp.GetComponent<KeyRegister>().keySetting;
        action = GetComponent<AnimatorTable>();
        player = ((Player)KBEngineApp.app.player());
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
    void OnEnable()
    {
        transform.position = entity.position;//在setActive时同步角色位置
    }
    void Update()
    {
        timeInterval += Time.deltaTime;
        int nowz = (int)transform.eulerAngles.z;
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos);
        mousePos.z = 19;
        mousePos= Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.up = -(mousePos - transform.position);
        if (nowz != lastZ && timeInterval >= UPDATE_Z_INTERVAL)
        {
           
            short ans = (short)(nowz);
            //Debug.Log("Z change nowz is"+nowz+" ans is" + ans);
            ((Player)entity).baseCall("updateZ", new object[] {ans});
            
            timeInterval = 0;
            lastZ = nowz;
        }
        if (entity != null)
        {
            entity.position=transform.position;
            entity.direction = transform.eulerAngles;
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
                changeV3.y += 5 * Time.deltaTime;
                //on_keyup_ing();
            }
            if (Input.GetKey(keySetting["left"]))
            {
                changeV3.x += 5 * Time.deltaTime;
                //on_keyleft_ing();
            }
            if (Input.GetKey(keySetting["down"]))
            {
                changeV3.y -= 5 * Time.deltaTime;
                //on_keydown_ing();
            }
            if (Input.GetKey(keySetting["right"]))
            {
                changeV3.x -= 5 * Time.deltaTime;
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
            //鼠標
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse = Input.mousePosition;
                mouse.z = 19;
                mouse = Camera.main.ScreenToWorldPoint(mouse);
                mouse.z = 0;
                Debug.Log("NetPlayerControler:position"+transform.position+"mouse Position"+mouse);
                on_left_down(mouse);
                Debug.Log("num"+on_left_down.GetInvocationList().Length+ "after mousePos is" + mouse);
            }
            transform.position += changeV3;

        //處理觸發事件
            while (eTriggerLine.Count > 0)
            {
                eTrigger temp = eTriggerLine[0];
                eList.equipments[temp.eIndex].trigger(temp.Args);
                eTriggerLine.RemoveAt(0);
            }
        }

    }
    //基本控制的
    void onKeyUpDown()
    {
        player.cellCall("notify1", new object[] {roomNo,CodeTable.KEYUP_DOWN});
        action.moveStart();
        upIng = true;
    }
    void onKeyDownDown()
    {
        //player.baseCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_DOWN });
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_DOWN });
        action.moveStart();
        downIng = true;
    }
    void onKeyLeftDown()
    {
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYLEFT_DOWN });
        action.moveStart();
        leftIng = true;
    }
    void onKeyRightDown()
    {
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYRIGHT_DOWN});
        action.moveStart();
        rightIng = true;
    }
    void onKeyUpUp()
    {
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYUP_UP });
        upIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
     }
    void onKeyDownUp()
    {
        //player.baseCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_UP });
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYDOWN_UP });
        downIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyLeftUp()
    {
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYLEFT_UP });
        leftIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onKeyRightUp()
    {
        player.cellCall("notify1", new object[] { roomNo, CodeTable.KEYRIGHT_UP });
        rightIng = false;
        if (!upIng && !leftIng && !downIng && !rightIng)
        {
            action.moveEnd();
        }
    }
    void onMouseLeftDown(Vector3 mousePos)
    {
        Debug.Log("on event mouse position is" + mousePos);
        player.cellCall("notify2", new object[] { roomNo,CodeTable.MOUSE_LEFT_DOWN,mousePos});
        action.AttackStart();
        player.cellCall("notify3", new object[] { 0, transform.position, mousePos });
        //0為普通攻擊的裝備索引
    }
    public void addOrder(Dictionary<string, object> item)
    {
        return;
    }

    public void addTriggerOrder(sbyte eIndex, Dictionary<string, object> args)
    {
        eTriggerLine.Add(new eTrigger(eIndex, args));
    }
}
