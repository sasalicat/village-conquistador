using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blood_suck : MonoBehaviour, Equipment
{
    private sbyte index;
    private RoleState state;
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
        this.state =state;
        if (attack.GetType().GetInterface("canBeAddition") != null)
        {
            Debug.Log("在onInit中canBeAddition:" + attack);
            ((canBeAddition)attack).onCauseDamage += trigger;
        }
    }

    public void trigger(Dictionary<string, object> args)
    {
        state.BeenTreat(state.gameObject, (int)(((damage)args["Damage"]).num * 0.2f));
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
