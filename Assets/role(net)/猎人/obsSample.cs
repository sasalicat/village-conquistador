using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsSample : MonoBehaviour,CDEquipment {

    sbyte index;
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
            throw new NotImplementedException();
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
            return 10;
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

    public bool Designated
    {
        get
        {
            return false;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        
    }

    public void setTime(float time)
    {
        
    }
    public float TimeLeft
    {
        get
        {
            return 0;
        }

        set
        {
        }
    }
    public void trigger(Dictionary<string, object> args)
    {
        NetManager.createObstacle(gameObject, transform.position, 1);
    }

    // Use this for initialization
    public void onCreateFinish(ObstacleState obstacle)
    {
        Debug.Log("onCreateFinish");
        if (obstacle.Kind ==1)
        {//如果障碍种类是本技能的则执行以下步骤
            ((obs1)obstacle).damage = new damage(1,100,0.5f);
        }
    }
}

