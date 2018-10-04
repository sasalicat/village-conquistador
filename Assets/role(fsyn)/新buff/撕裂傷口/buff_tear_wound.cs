using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buff_tear_wound : Buff
{
    public int layer = 1;
    private const float add_per_layer=0.1f;
    private const int effIndex = 48;
    private const int max_layer = 10;
    public GameObject eff;
    public override float Duration
    {
        get
        {
            return 2.5f;
        }
    }
    public void onTakeDamage(Dictionary<string,object> arg)
    {
        damage d= ((damage)arg["Damage"]);
        if(d.kind==damage.PHY_DAMAGE)
            d.num = ((int)(d.num * (1 + layer * 0.1f)));
    }
    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable)
    {
        if (Repetitive != null)
        {
            if (Repetitive.Length > 0)
            {
                var thatbuff = ((buff_tear_wound)Repetitive[0]);
                int count = thatbuff.layer;
                if (count < max_layer)
                {
                    thatbuff.layer += 1;
                    thatbuff.timeLeft = Duration;
                }
                return false;
            }
        }
        eff= Instantiate(misTable.MissileList[effIndex]);
        eff.GetComponent<followGameObject>().offset = new Vector2(0,3.5f);
        eff.GetComponent<followGameObject>().master = gameObject;
        GetComponent<Controler>().On_Take_Damage+=onTakeDamage;
        return true;
    }

    public override void onRemove(RoleState role)
    {
        
    }
    void OnDestroy()
    {
        Debug.Log("buff_tear_wound OnDestroy 被呼叫");
        Destroy(eff);
    }
}
