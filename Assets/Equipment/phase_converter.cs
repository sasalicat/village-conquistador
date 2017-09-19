using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phase_converter : MonoBehaviour, CDEquipment
{
    public const float CD = 0.4f;//0.5f;
    public const int BaseDamage = 0;
    public const float BaseStiff = 0f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    ObstacleState obstacle1 = null;
    Vector3 backPosition;

    //實做Equipment介面-------------------------------------------------------
    public sbyte No
    {
        get
        {
            return 0;
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

    public sbyte Kind//本技能属于主动技能所以kind为 PASSIVE_SKILL
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }
    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
    }
    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
        }
    }
    public float TimeLeft
    {
        get
        {
            return CDTime;
        }

        set
        {
            CDTime = value;
        }
    }
    public uint Consumption
    {
        get
        {
            return 0;//因為是攻擊所以無消耗
        }
    }

    public bool Designated
    {
        get
        {
            return false;
        }
    }

    //----------------------------------------------------------------------


    public void trigger(Dictionary<string, object> args)
    {
        if (obstacle1 == null)
        {
            getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
            Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
            Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
            //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
            Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
            backPosition = origenPlayerPosition;

            //創建障礙物
            NetManager.createObstacle(gameObject, origenPlayerPosition, 5);
        }else
        {
            selfState.transform.position = backPosition;
            obstacle1.DestoryObjInServer();
        }

        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        selfState = state;
    }

    public void onCreateFinish(ObstacleState obstacle)
    {
        obstacle1 = obstacle;
    }
}
