using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yao_buff : Buff {
    public int level=0;
    public override float Duration
    {
        get
        {
            return 15;
        }
    }

    public override bool onInit(RoleState role, Buff[] Repetitive, MissileTable misTable)
    {
        if (Repetitive == null)
        {
            level += 1;
            role.Power += 10;
            role.Skill += 10;
            return true;
        }
        if (Repetitive.Length > 0)
        {
            yao_buff buff = (yao_buff)Repetitive[0];
            if (buff.level < 5)
            {//每層buff增加10點力量10點智力
                buff.level += 1;
                role.Power += 10;
                role.Skill += 10;
            }
            //刷新buff時間
            buff.timeLeft = 15;
            return false;
        }
        else
        {
            level += 1;
            role.Power += 10;
            role.Skill += 10;
        }
        return true;
    }

    public override void onRemove(RoleState role)
    {
        role.Power -= level * 10;
        role.Skill -= level * 10;
    }

}
