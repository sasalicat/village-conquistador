using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class yao_buff : Buff {
    public int level=0;
    private GameObject token;
    public Text LVtext = null;
    public int MAX_LEVEL = 10;
    public override float Duration
    {
        get
        {
            return 20;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable, Dictionary<string, object> args)
    {
        if (Repetitive == null)
        {
            level += 1;
            role.Power += 10;
            role.Skill += 10;
            
            token = Instantiate(misTable.MissileList[40], role.transform.position, role.transform.rotation, role.transform);
            token.transform.localPosition = new Vector3(0, 3, 0);
            LVtext = token.transform.Find("面板/层数").GetComponent<Text>();
            LVtext.text = "1";
                return true;
        }
        if (Repetitive.Length > 0)
        {
            yao_buff buff = (yao_buff)Repetitive[0];
            if (buff.level < MAX_LEVEL)
            {//每層buff增加10點力量10點智力
                buff.level += 1;
                role.Power += 10;
                role.Skill += 10;
                buff.LVtext.text = buff.level + "";
            }
            //刷新buff時間
            buff.timeLeft = 20;
            return false;
        }
        else
        {
            level += 1;
            role.Power += 10;
            role.Skill += 10;
            token = Instantiate(misTable.MissileList[40], role.transform.position, role.transform.rotation, role.transform);
            token.transform.localPosition = new Vector3(0, 3, 0);
            LVtext = token.transform.Find("面板/层数").GetComponent<Text>();
            LVtext.text = "1";
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.Power -= level * 10;
        role.Skill -= level * 10;
        Destroy(token);
    }

}
