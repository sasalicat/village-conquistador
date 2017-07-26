using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treatment_halo : MonoBehaviour, Equipment
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
            return EquipmentTable.ON_INTERVAL;
        }
    }

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        this.roleState = state;
        missilePraf = table.MissileList[12];
        GameObject newone = Instantiate(missilePraf, transform.position, transform.rotation, transform);
        missile = newone.GetComponent<Missile>();
        missile.Creater = gameObject;
    }

    public void trigger(Dictionary<string, object> args)
    {
        float interval = (float)args["interval"];
        if((time -= interval) <= 0)
        {
            ((mis_treatment_halo)missile).BeenTreat();
            time = 1;
        }
    }

}
