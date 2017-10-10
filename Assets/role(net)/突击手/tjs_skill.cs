using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tjs_skill : MonoBehaviour, CDEquipment
{

    public const float CD = 2f;//0.5f;
    public const int BaseDamage = 50;
    public const float BaseStiff = 0.25f;

    public float CDTime = 0;
    public sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    public Text Label;

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
            //return EquipmentTable.ON_TAKE_DAMAGE;
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
            return 15;//因為是攻擊所以無消耗
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

        getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        //使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標
        //制造子弹物件
        Vector3 direction = mousePosition - origenPlayerPosition;
        //GameObject newone = Instantiate(missilePraf, tragetPos, this.transform.rotation);
        //missilePraf.transform.forward = direction;
        //missilePraf.transform.eulerAngles = new Vector3(0, 0, missilePraf.transform.eulerAngles.z);

        GameObject newone1 = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
        GameObject newone2 = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
        GameObject newone3 = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
        GameObject newone4 = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
        GameObject newone5 = Instantiate(missilePraf, tragetPos, Quaternion.Euler(direction));
        newone1.transform.up = direction;
        newone2.transform.up = direction;
        newone3.transform.up = direction;
        newone4.transform.up = direction;
        newone5.transform.up = direction;
        newone2.transform.eulerAngles = new Vector3(0, 0, newone2.transform.eulerAngles.z + 45);
        newone3.transform.eulerAngles = new Vector3(0, 0, newone3.transform.eulerAngles.z + 90);
        newone4.transform.eulerAngles = new Vector3(0, 0, newone4.transform.eulerAngles.z - 45);
        newone5.transform.eulerAngles = new Vector3(0, 0, newone5.transform.eulerAngles.z - 90);
        //修改子弹物件携带的子弹脚本
        Missile missile1 = newone1.GetComponent<Missile>();
        Missile missile2 = newone2.GetComponent<Missile>();
        Missile missile3 = newone3.GetComponent<Missile>();
        Missile missile4 = newone4.GetComponent<Missile>();
        Missile missile5 = newone5.GetComponent<Missile>();
        missile1.Creater = gameObject;
        missile2.Creater = gameObject;
        missile3.Creater = gameObject;
        missile4.Creater = gameObject;
        missile5.Creater = gameObject;
        //创建伤害物件
        unit u = this.GetComponent<unit>();
        int num = Attribute.GetAttackDamageNum(30, u.power);
        float stiff = Attribute.getRealStiff(0.1f, u.stiffable);
        missile1.Damage = new damage(2, num, stiff, false, false, gameObject);
        missile2.Damage = new damage(2, num, stiff, false, false, gameObject);
        missile3.Damage = new damage(2, num, stiff, false, false, gameObject);
        missile4.Damage = new damage(2, num, stiff, false, false, gameObject);
        missile5.Damage = new damage(2, num, stiff, false, false, gameObject);


        CDTime = CD;//技能冷卻
        //Debug.Log("in trigger CDTime is" + CDTime);


    }


    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        //初始化赋值
        missilePraf = table.MissileList[2];
        this.selfState = state;
    }
}