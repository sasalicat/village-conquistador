using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//基础按键事件---------------------------------------------
              /*技能按键*/
public delegate void _on_skill_key_down(Vector3 mousePos,sbyte keyCode);
              /*移动按键*/
public delegate void _on_keyup_down();//按下 上键
public delegate void _on_keyup_ing();//按住 上键
public delegate void _on_keyup_up();//松开 上键
public delegate void _on_keyright_down();
public delegate void _on_keyright_ing();
public delegate void _on_keyright_up();
public delegate void _on_keydown_down();
public delegate void _on_keydown_ing();
public delegate void _on_keydown_up();
public delegate void _on_keyleft_down();
public delegate void _on_keyleft_ing();
public delegate void _on_keyleft_up();
//被动效果触发事件----------------------------------------
public delegate void up_press_on();
public delegate void _on_trigger(Dictionary<string,object> arg);

public interface Controler
{
    _on_skill_key_down get_on_left_down();
    _on_skill_key_down get_on_right_down();
    _on_skill_key_down get_on_middle_down();
    _on_skill_key_down get_on_key1_down();
    _on_skill_key_down get_on_key2_down();
    _on_skill_key_down get_on_key3_down();
    _on_skill_key_down get_on_key4_down();
    _on_skill_key_down get_on_key5_down();
    /*移动按键*/
    _on_keyup_down get_on_keyup_down();
    _on_keyup_ing get_on_keyup_ing();
    _on_keyup_up get_on_keyup_up();
    _on_keyright_down get_on_keyright_down();
    _on_keyright_ing get_on_keyright_ing();
    _on_keyright_up get_on_keyright_up();
    _on_keydown_down get_on_keydown_down();
    _on_keydown_ing get_on_keydown_ing();
    _on_keydown_up get_on_keydown_up();
    _on_keyleft_down get_on_keyleft_down();
    _on_keyleft_ing get_on_keyleft_ing();
    _on_keyleft_up get_on_keyleft_up();
    /*装备事件*/

    _on_trigger On_Take_Damage
    {
        set;
        get;
    }
    _on_trigger On_Interval
    {
        set;
        get;
    }
    _on_trigger On_Been_Treat
    {
        set;
        get;
    }
    _on_trigger On_Hp_Change
    {
        set;
        get;
    }
    _on_trigger On_MP_Change
    {
        set;
        get;
    }
    _on_trigger On_Cause_Damage
    {
        set;
        get;
    }
    _on_trigger On_Active_Skill
    {
        set;
        get;
    }
    int Index
    {
        set;
        get;
    }
    List<sbyte> skillLimit//里面放的是可以且仅可以使用的技能编号,如果不为空则只能使用list里面编号的技能,若为空则没有限制
    {
        set;
    }
    void addBuffByNo(sbyte no);
    void distortionByNo(sbyte no);
}
