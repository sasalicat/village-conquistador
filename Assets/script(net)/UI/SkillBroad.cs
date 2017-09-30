using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBroad : MonoBehaviour {
    public GameObject prafeb;
    GameObject[] Lables = new GameObject[10];
    skillLabel[] controls = new skillLabel[10];
    private bool first = false;
    private EquipmentList maine;
    private IconStorage storage;
    public EquipmentList mainRoleElist
    {
        set
        {
            maine = value;
            first = true;
        }
        get
        {
            return maine;
        }
    }
    public void AddLabel(int index)
    {
        Lables[index] = Instantiate(prafeb,this.transform);
    }
    public void changeAllLabel(EquipmentList.ArmedHarness arm)
    {
        Debug.Log("in changeAllLabel Label length is:" + Lables.Length);
        for (int i = 0; i < Lables.Length; i++)
        {
             if (Lables[i] != null)
            {
                Destroy(Lables[i]);
            }
        }
        Debug.Log("in changeAllLabel passive length is:" + arm.passiveEquipments.Count);

        for (int i = 0;i < arm.passiveEquipments.Count;i++)
        {

            AddLabel(i);
            controls[i]= Lables[i].GetComponent<skillLabel>();
            controls[i].storage = storage;
            controls[i].Init(arm.passiveEquipments[i].No, arm.passiveEquipments[i].TimeLeft);
        }
    }
	// Use this for initialization
	void Start () {
        storage = GameObject.Find("Icons").GetComponent<IconStorage>();

    }
	
	// Update is called once per frame
	void Update () {
        if (first)
        {
             changeAllLabel(mainRoleElist.nowHarness);
            first = false;
        }
        //Debug.Log("nowHarness size:" + mainRoleElist.nowHarness.passiveEquipments.Count);
        for (int i= 0;i<controls.Length;i++)
        {
            if(controls[i]!=null)
                controls[i].updateTime(mainRoleElist.nowHarness.passiveEquipments[i].TimeLeft);
        }

	}
}
