using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderState : RoleState,Controler {
    public GameObject Creater;
    public int Kind;
    public int nullCallTimes = 0;
    public List<sbyte> sbyteCalldatas = new List<sbyte>();

    public void empty(Dictionary<string, object> useless)
    {

    }

    class obs_normal : state
    {
        private RoleState obs;
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
                return 0;
            }
        }

        public void beenTreat(int num, GameObject from)
        {
            return;
        }



        public void takedamage(damage damage)
        {
           
        }

        public void onUpdate()
        {
        }

        public obs_normal(RoleState obs)
        {
            this.obs = obs;
        }
    }
    // Use this for initialization
    override protected void Start()
    {
        base.Start();
        StateTable = new List<state>();
        nowState = new obs_normal(this);
        this.control = this;
    }

    // Update is called once per frame
    protected void Update()
    {
        nowState.onUpdate();
    }
    public virtual void methodNull()
    {
        Debug.Log("父類別methodnull");
    }
    public virtual void methodSbyte(sbyte data)
    {

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
        return null;
    }

    public _on_skill_key_down get_on_right_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_middle_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_key1_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_key2_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_key3_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_key4_down()
    {
        return null;
    }

    public _on_skill_key_down get_on_key5_down()
    {
        return null;
    }

    public _on_keyup_down get_on_keyup_down()
    {
        return null;
    }

    public _on_keyup_ing get_on_keyup_ing()
    {
        return null;
    }

    public _on_keyup_up get_on_keyup_up()
    {
        return null;
    }

    public _on_keyright_down get_on_keyright_down()
    {
        return null;
    }

    public _on_keyright_ing get_on_keyright_ing()
    {
        return null;
    }

    public _on_keyright_up get_on_keyright_up()
    {
        return null;
    }

    public _on_keydown_down get_on_keydown_down()
    {
        return null;
    }

    public _on_keydown_ing get_on_keydown_ing()
    {
        return null;
    }

    public _on_keydown_up get_on_keydown_up()
    {
        return null;
    }

    public _on_keyleft_down get_on_keyleft_down()
    {
        return null;
    }

    public _on_keyleft_ing get_on_keyleft_ing()
    {
        return null;
    }

    public _on_keyleft_up get_on_keyleft_up()
    {
        return null;
    }

    void Controler.addBuffByNo(sbyte no)
    {
        
    }

    void Controler.distortionByNo(sbyte no)
    {
       
    }

    public bool equipmentReady(sbyte eindex)
    {
        return false;
    }
    public void beShift(Vector3 speed, float time)
    {
        throw new NotImplementedException();
    }
}
