using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingManager : MonoBehaviour,Manager {
    public PrabTabel prafebTable;
    public dataRegister register;
    private GameObject[] roleObjList=new GameObject[20];
    public float nextInterval = 0.1f;
    public GameObject[] getGameObjectList()
    {
        return roleObjList;
    }

    public int getMaxNum()
    {
        return 1;
    }

    // Use this for initialization
    void Start () {
        //先把当前角色加入PlayerInWar
       

        prafebTable = GameObject.Find("keyTabel").GetComponent<PrabTabel>();
        register = GameObject.Find("client").GetComponent<dataRegister>();
        register.PlayerInWar[0] = new dataRegister.PlayerData(register.nowRoleData.roleKind, register.nowRoleData.equipmentIdList, "主角", true, 1);

        RoleData roledata=register.nowRoleData;
        roleObjList[0]= Instantiate(prafebTable.table[roledata.roleKind],new Vector3(0,0,0),this.transform.rotation);

       
        RoleState state= (RoleState)roleObjList[0].AddComponent(Type.GetType("RoleState"));

        trainingControler control = roleObjList[0].AddComponent<trainingControler>();
        control.Index = 0;
        control.state = state;

        roleObjList[0].GetComponent<EquipmentList>().controler = control;
        roleObjList[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
