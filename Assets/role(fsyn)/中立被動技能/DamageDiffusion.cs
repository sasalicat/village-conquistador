using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageDiffusion : MonoBehaviour,Equipment {
    private sbyte index;
    private MissileTable misTable;
    private const int misIndex=49;
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
            return 68;
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
    public void onCreateMis(addibleMissile mis,bool ori)
    {
        mis.onHit += onMisHit;
    }
    public void onMisHit(GameObject traget, addibleMissile self)
    {
        GameObject newone= Instantiate(misTable.MissileList[misIndex],traget.transform.position,traget.transform.rotation);
        newone.transform.eulerAngles = new Vector3(0,0,traget.transform.eulerAngles.z+180);
        var newD = new damage(self.Damage.kind,self.Damage.num/2,0,false,false,self.Damage.damager);
        newone.GetComponent<mis_DamageDiffusion>().Damage = newD;
        newone.GetComponent<mis_DamageDiffusion>().oriEnemy = traget;
    }
    public void onInit(MissileTable table, RoleState state, AnimatorTable anim)
    {
        EquipmentList eList = state.GetComponent<EquipmentList>();
        ((canBeAddition)eList.nowHarness.passiveEquipments[0]).onCreateMissile+= onCreateMis;
        misTable = table;
    }

    public void trigger(Dictionary<string, object> args)
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
