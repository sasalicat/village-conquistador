using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitTask : MonoBehaviour,CDEquipment {
    public GameObject show;//预设体
    public GameObject has;//已经创建出来的物件
    public bool active=false;
    public Controler controler;
    private sbyte index;
    public bool CanUse
    {
        get
        {
            return true;
        }
    }

    public uint Consumption
    {
        get
        {
            return 0;
        }
    }

    public bool Designated
    {
        get
        {
            return false;
        }
    }

    public sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }

    public sbyte No
    {
        get
        {
            return 35;
        }
    }

    public sbyte selfIndex
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

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        show = table.MissileList[9];
        controler = GetComponent<Controler>();
    }

    public void setTime(float time)
    {
        
    }

    public void trigger(Dictionary<string, object> args)
    {
        if (has == null)//如果没有has先创造has
        {
            has = Instantiate(show, transform.position, transform.rotation, transform);
        }
        if (active)
        {
            active = false;
            has.SetActive(false);
            controler.skillLimit = null;//解除技能限制
        }
        else
        {
            active = true;
            has.SetActive(true);
            controler.skillLimit = new List<sbyte> { index };//设置技能限制到本技能
        }

    }


}
