using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalDetermination : MonoBehaviour, CDEquipment
{
    public const float CD = 0.4f;//4f;
    public const int BaseDamage = 100;//80
    public const float BaseStiff = 0.5f;
    public float second = 0f;

    public float CDTime = 0;
    public sbyte index;
    private RoleState selfState;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    GameObject gameObjectImmortal = null;
    delegate void TakeDamage(Dictionary<string, object> arg);
    KBControler kbcontroler;
    bool flag = true;

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
        second -= time;
        if (second <= 0)
        {
            if(!flag)
            {
                kbcontroler.On_Take_Damage -= On_Take_Damage;
                second = 8;
                flag = true;
                Debug.Log("kbcontroler.On_Take_Damage -= On_Take_Damage");
                Destroy(gameObjectImmortal);
            }
        }
    }

    public bool CanUse
    {
        get
        {
            Debug.Log(" in can use CDTime is" + CDTime);
            return (CDTime <= 0);//如果CDTime小於0代表技能可以使用
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
            return true;
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

    public void trigger(Dictionary<string, object> args)
    {
        ((GameObject)args["Traget"]).GetComponent<Controler>().addBuffByNo(2);
        Vector3 v = new Vector3(1.2f, 0f, 0f);
        gameObjectImmortal = Instantiate(missilePraf, transform.position, transform.rotation);
        gameObjectImmortal.transform.parent = transform;
        kbcontroler = gameObject.GetComponent<KBControler>();
        kbcontroler.On_Take_Damage += On_Take_Damage;
        second = 8;
        flag = false;

        Debug.Log("On_Take_Damage 1");

        second = 8;
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        selfState = state;
        missilePraf = table.MissileList[22];
    }

    public void On_Take_Damage(Dictionary<string, object> arg)
    {
        damage damage1 = (damage)arg["Damage"];
        Debug.Log("damage 1" + damage1.num);
        damage1.num = (int)(damage1.num * (1 - 20 / 100f));
        Debug.Log("damage 2"+damage1.num);
        damage1.stiffTime = 0;
        Debug.Log("On_Take_Damage 2");
    }
}