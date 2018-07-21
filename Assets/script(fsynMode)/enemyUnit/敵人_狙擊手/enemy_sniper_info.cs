using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_sniper_info : enemyInfo
{
    public override int getBaseHp(int level)
    {
        return 300 + ((int)(3000 * ((float)level / 100)));
    }
    public override void initUnit(RoleState role, int level)
    {
        base.initUnit(role, level);
        role.GetComponent<Animator>().SetFloat("attackSpeed", 0.35f);
    }
}
