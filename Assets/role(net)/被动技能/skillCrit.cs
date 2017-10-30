using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillCrit : MonoBehaviour, Equipment
{
    public sbyte index;
    RoleState roleState;
    private GameObject missilePraf;//暫存總missileTable內得到的預設體
    float time = 1f;
    Missile missile;
    public sbyte No
    {
        get
        {
            return 33;
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
            return EquipmentTable.ON_CAUSE_DAMAGE;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.roleState = state;
    }

    public void trigger(Dictionary<string, object> args)
    {
        Debug.Log("GameObject~~~   " + gameObject);
        sbyte random = (sbyte)args["randomPoint"];
        damage damage1 = (damage)args["Damage"];
        if (damage1.kind == 2 && random <= 30)
        {
            damage1.num = 2 * damage1.num;
        }
        Debug.Log("Damage~~~~~  " + (int)((float)damage1.num * 0.2) + "  " + roleState.gameObject.name);
    }

}
