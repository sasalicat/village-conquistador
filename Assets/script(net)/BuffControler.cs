using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffControler : MonoBehaviour {
    public const int MAX_BUFF_NUM = 33;
    Buff[] buffInRole = new Buff[MAX_BUFF_NUM];
    public RoleState roleState;
    public MissileTable misTable;
	// Use this for initialization
	void Start () {
        roleState = GetComponent<RoleState>();
        GetComponent<Controler>().On_Interval += Intarvel;
        misTable = GameObject.Find("keyTabel").GetComponent<MissileTable>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Intarvel(Dictionary<string, object> arg)
    {
        float time = (float)arg["interval"];
        foreach(Buff item  in buffInRole)
        {
            if(item!=null)
                item.onIntarvel(roleState,time);
        }
    }
    public Buff AddBuff(string buffName)
    {


        var a = GetComponents(Type.GetType(buffName));


        Component[] has = GetComponents(Type.GetType(buffName));
        Buff[] buffs=null ;
        Debug.Log("in AddBuff has is" + has+"length:"+has.Length);
        if (has.Length>0)
        {
            buffs = new Buff[has.Length];
            for(int i=0;i<has.Length; i++)
            {
                buffs[i] = (Buff)has[i];
            }
        }
        Buff buff= (Buff)gameObject.AddComponent(Type.GetType(buffName));
        buff.onInit(roleState, buffs,misTable);
        for (int i = 0; i < buffInRole.Length; i++) {
            Debug.Log("buff in role i:" + i);
            if (buffInRole[i] == null)
            {
                buffInRole[i] = buff;
                buff.index = i;
                break;
            }
        }
        return buff;
    }
}
