using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mg_atk : MonoBehaviour,PassiveEquipment
{
    sbyte index;
    const short selfMissileNo = 0;
    public GameObject missilePraf;
    public MissileTable table;
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
    void Start()
    {
        table = GameObject.Find("keyTabel").GetComponent<MissileTable>();
        //Debug.Log(table);
        missilePraf = table.MissileList[0];
    }
    public sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }
    public void passiveSkill(Vector3 clickPos)
    {
        Debug.Log("enter trigger");
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        NetManager.SkillTrigger(selfIndex,pos,mousePos);

    }

    public void trigger(Dictionary<string, object> args)
    {
       getVector getVector= GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        Debug.Log("In trigger:position:"+origenPlayerPosition+"mouse position:"+mousePosition);
        Vector3 tragetPos=getVector.getOriginalInitPoint(origenPlayerPosition,mousePosition,new Vector3(0,-1,0));
        Instantiate(missilePraf, tragetPos, transform.rotation);
    }

}
