using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class ObstacleState : RoleState,Controler {//相当于控制器和State结合在同一个脚本
    public Entity entity;
    public  GameObject Creater;
    public int Kind;
    public int nullCallTimes = 0;
    public List<sbyte> sbyteCalldatas = new List<sbyte>();
    
    public void empty(Dictionary<string, object> useless)
    {

    }

    class obs_normal : state
    {
        private ObstacleState obs;
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

        public void onUpdate()
        {
            while (obs.nullCallTimes > 0)
            {
                Debug.Log("method null从服务器呼叫 times:"+ obs.nullCallTimes);
                obs.methodNull();
                obs.nullCallTimes--;
            }
            while (obs.sbyteCalldatas.Count > 0)
            {
                obs.methodSbyte(obs.sbyteCalldatas[0]);
                obs.sbyteCalldatas.RemoveAt(0);
            }
        }

        public void takedamage(damage damage)
        {
            if (damage.damager.GetComponent<NetPlayerControler>() != null)
            {
                obs.entity.cellCall("reduceHp", new object[] { (short)damage.num });
            }
        }
        public obs_normal(ObstacleState obs)
        {
            this.obs = obs;
        }
    }
    // Use this for initialization
      override protected void Start () {
        base.Start();
        StateTable = new List<state>();
        nowState = new obs_normal(this);
        this.control = this;
    }
	
	// Update is called once per frame
	protected void Update () {
        nowState.onUpdate();
	}
    public virtual void methodNull()
    {
        Debug.Log("父類別methodnull");
    }
    public virtual void methodSbyte(sbyte data)
    {

    }
    public void callMethodNull()
    {
        if (Creater.GetComponent<NetRoleState>().islocal)
        {
            Debug.Log("呼叫服务器nullmethod");
            entity.cellCall("method_Null");
        }
    }
    public void callMethodSbyte(sbyte data)
    {
        if (Creater.GetComponent<NetRoleState>().islocal)
        {
            entity.cellCall("methodSbyte",new object[] {data});
        }
    }
    public void DestoryObjInServer()
    {
        entity.cellCall("DestoryEntity");
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
