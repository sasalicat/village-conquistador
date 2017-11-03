using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bone_shield : MonoBehaviour, CDEquipment
{
    public const float CD = 2f;//0.5f;
    public const int BaseDamage = 0;
    public const float BaseStiff = 4f;

    public float CDTime = 2f;
    public sbyte index;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    private RoleState selfState;
    List<GameObject> newone = new List<GameObject>();
    damage damage1;

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
            return EquipmentTable.ON_TAKE_DAMAGE;
        }
    }

    //實做CDEquipment介面----------------------------------------------
    public void setTime(float time)
    {
        CDTime -= time;//減少CD時間
        if (CDTime <= 0)
        {
            if(newone.Count < 5) { 
                GameObject newCharater = Instantiate(missilePraf, transform.position, this.transform.rotation);
                newCharater.transform.eulerAngles = new Vector3(0,0,270);
                (newCharater.GetComponent<RotateAround>()).aroundPoint = gameObject.transform;
                (newCharater.GetComponent<RotateAround>()).angled = 0;
                newCharater.SetActive(true);
                newone.Add(newCharater);

                if (newone.Count > 1)
                {
                    RotateAround rotateAround1 = newone[newone.Count - 2].GetComponent<RotateAround>();
                    RotateAround rotateAround2 = newone[newone.Count - 1].GetComponent<RotateAround>();
                    rotateAround2.angled = rotateAround1.angled + 70;
                }
            }
            CDTime = 2;
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
        //getVector getVector = GameObject.Find("keyTabel").GetComponent<getVector>();
        //Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        //Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        ////使用getOriginalInitPoint得到技能在client端创建物件的正确位置
        //Vector3 tragetPos = getVector.getOriginalInitPoint(origenPlayerPosition, mousePosition, new Vector3(0, -1, 0));//獲得相對座標

        damage1 = (damage)args["Damage"];
        if(newone.Count > 0)
        {
            damage1.num = (int)(damage1.num * (1 - newone.Count / 10.0f));
            Destroy(newone[newone.Count - 1]);
            newone.RemoveAt(newone.Count - 1);
        }


        CDTime = CD;//技能冷卻
        Debug.Log("in trigger CDTime is" + CDTime);
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        missilePraf = table.MissileList[13];
    }
}