using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dizziness_hit : MonoBehaviour, Equipment
{
    private sbyte index;
    public sbyte Kind
    {
        get
        {
            return EquipmentTable.NO_TRIGGER;
        }
    }

    public sbyte No
    {
        get
        {
            return 69;
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

    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        var eList = state.GetComponent<EquipmentList>();
        var attack = eList.nowHarness.passiveEquipments[EquipmentList.ATK];
        if (attack.GetType().GetInterface("canBeAddition") != null)
        {
            Debug.Log("在onInit中canBeAddition:" + attack);
            ((canBeAddition)attack).onCauseDamage += trigger;
        }
    }

    public void trigger(Dictionary<string, object> args)
    {
        if (((int)args["randomPoint"])>50&& ((int)args["randomPoint"])<=70)
        {
            damage damage = (damage)args["Damage"];
            damage.stiffTime += 0.8f;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
