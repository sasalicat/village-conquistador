using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mg_atk : MonoBehaviour,CDEquipment
{
    public const float CD = 0.5f;


    private float CDTime = 0; 
    sbyte index;
    const short selfMissileNo = 0;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    public MissileTable table;
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

    public sbyte Kind
    {
        get
        {
            return EquipmentTable.PASSIVE_SKILL;
        }
    }
//實做CDEquipment介面----------------------------------------------
    public void getTime(float time)
    {
        CDTime -= time;//減少CD時間
    }
    public bool CanUse
    {
        get
        {
            return CDTime <= 0;//如果CDTime小於0代表技能可以使用
        }
    }
    public uint Consumption
    {
        get
        {
            return 0;//因為是攻擊所以無消耗
        }
    }
//----------------------------------------------------------------------
   

    public void trigger(Dictionary<string, object> args)
    {
        Label.text = "in index" + selfIndex + "trigger start";
       getVector getVector= GameObject.Find("keyTabel").GetComponent<getVector>();
        Vector3 origenPlayerPosition = (Vector3)args["PlayerPosition"];//施放技能時玩家位置
        Vector3 mousePosition = (Vector3)args["MousePosition"];//施放技能時鼠標點擊位置
        Debug.Log("In trigger:position:"+origenPlayerPosition+"mouse position:"+mousePosition);
        Vector3 tragetPos=getVector.getOriginalInitPoint(origenPlayerPosition,mousePosition,new Vector3(0,-1,0));//獲得相對座標
        Instantiate(missilePraf, tragetPos, transform.rotation);
        CDTime = CD;//技能冷卻
        Label.text = "in index" + selfIndex + "trigger end";
    }
    void Start()
    {
        table = GameObject.Find("keyTabel").GetComponent<MissileTable>();
        //Debug.Log(table);
        missilePraf = table.MissileList[0];
        Label= Label = GameObject.Find("Canvas/Text2").GetComponent<Text>();
    }

}
