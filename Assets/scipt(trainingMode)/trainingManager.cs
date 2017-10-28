using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingManager : MonoBehaviour,Manager {
    public PrabTabel prafebTable;
    public dataRegister register;
    private GameObject[] roleObjList=new GameObject[20];
    private Controler[] controlers = new Controler[20];
    private EquipmentList[] elists = new EquipmentList[20];
    public float nextInterval = 0.1f;
    public HpBarManager hpManage;
    public FloatingManager Floating;
    public SkillBroad broad;//手動拉取賦值
    public GameObject leavePage;//手動拉取too

    //暂时的陪练游戏物件
    public GameObject AttackOnly;
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
        register.PlayerInWar[1] = new dataRegister.PlayerData(0,new List<sbyte> {0},"攻击型沙袋",true,2);

        RoleData roledata=register.nowRoleData;
        roleObjList[0]= Instantiate(prafebTable.table[roledata.roleKind],new Vector3(0,0,0),this.transform.rotation);

       
        RoleState state= (RoleState)roleObjList[0].AddComponent(Type.GetType("RoleState"));

        trainingControler control = roleObjList[0].AddComponent<trainingControler>();
        controlers[0] = control;
        control.Index = 0;
        control.state = state;

        elists[0] = roleObjList[0].GetComponent<EquipmentList>();
        elists[0].controler = control;
        roleObjList[0].SetActive(true);
        hpManage.CreateHpBar(roleObjList[0],0);
        broad.mainRoleElist = roleObjList[0].GetComponent<EquipmentList>();//設置技能顯示
        roleObjList[0].GetComponent<EquipmentList>().on_AH_change += broad.changeAllLabel;
        //创建攻击陪练暂时先这样
        roleObjList[1] = Instantiate(AttackOnly, new Vector3(-10, 10, 0), this.transform.rotation);
        controlers[1] = roleObjList[1].GetComponent<trainingBase>();
        Debug.Log("controler:"+controlers[1]);
        controlers[1].Index = 1;
        elists[1] = roleObjList[1].GetComponent<EquipmentList>();
        elists[1].controler = controlers[1];
        roleObjList[1].SetActive(true);
        hpManage.CreateHpBar(roleObjList[1], 1);


    }
	
	// Update is called once per frame
	void Update () {
        nextInterval -= Time.deltaTime;
        if (nextInterval <= 0)
        {
            nextInterval = 0.1f;
            foreach(Controler c in controlers)
            {
                if (c!=null) {
                    Debug.Log("c is " + c);
                    Dictionary<string, object> newData = new Dictionary<string, object>();
                    newData["interval"] = NetManager.INTERVAL_CYCLE;
                    if (c.On_Interval != null)
                    {
                        c.On_Interval(newData);
                    }
                }
                else
                {
                    break;
                }
                
            }
            foreach(EquipmentList el in elists)
            {
                if (el != null)
                {
                    el.allReduceCD(NetManager.INTERVAL_CYCLE);
                }
                else
                {
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            leavePage.SetActive(true);
        }
	}
}
