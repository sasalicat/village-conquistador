using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fsynManager_local : MonoBehaviour, Manager {
    private int MAX_UNIT_NUM = 10;
    public float SINGLE_FRAME_TIME = 0.03f;
    public static fsynManager_local main;
    public GameObject[] objList;

    public Dictionary<int,GameObject> enemyList;
    private int enemyRecondNum = 0;

    private fsynControler[] controlList;
    private EquipmentTable eTable;
    private PrabTabel prabTable;
    public sceneScript script;
    public HpBarManager hpBarCreater;//手动拉取赋值
    List<OrderPoor> playerPoors;
    public class OrderPoor
    {
        public int unitNo;
        public List<Dictionary<string, object>> orders;//orders長度為0時代表該玩家這一幀的指令還沒有到達
        public OrderPoor(int no)
        {
            unitNo = no;
            orders = new List<Dictionary<string, object>>();
        }
    }
    public void addOrderFor(int playerNo,Dictionary<string,object> order)
    {
        playerPoors[playerNo].orders.Add(order);
    }
    public GameObject[] getGameObjectList()
    {
        return objList;
    }

    public int getMaxNum()
    {
        return MAX_UNIT_NUM;
    }
    void OnEnable()
    {
        if (main != null)
        {
            Destroy(this);
        }
        else
        {
            main = this;
        }
    }

    // Use this for initialization
    void Start () {
        objList = new GameObject[MAX_UNIT_NUM];
        controlList = new fsynControler[MAX_UNIT_NUM];
        enemyList = new Dictionary<int, GameObject>();
        GameObject table = GameObject.Find("keyTabel");
        eTable = table.GetComponent<EquipmentTable>();
        prabTable = table.GetComponent<PrabTabel>();
        script = GetComponent<sceneScript>();
        playerPoors = new List<OrderPoor>();
        PostOffice.main.cycleTime = SINGLE_FRAME_TIME;

        script.onStart(this);
    }
    public void createMainRole(int rno, int roleKind, List<int> eList, Vector2 pos)
    {
        createMainRole(rno, roleKind, eList, pos, true);
    }
    public void createMainRole(int rno, int roleKind, List<int> eList, Vector2 pos,bool mainrole)
    {
        GameObject nowRole = Instantiate(prabTable.table[roleKind], pos, transform.rotation);
        controlList[rno] = nowRole.AddComponent<fsynControler>();
        controlList[rno].Index = rno;
        var state= nowRole.AddComponent<NetRoleState>();
        nowRole.SetActive(true);
        var equiplist = nowRole.GetComponent<EquipmentList>();
        equiplist.controler = controlList[rno];
        equiplist.Start();
        hpBarCreater.CreateHpBar(nowRole, rno, "你", "超威藍貓");
        foreach(int index in eList)
        {
            equiplist.addByNo(index);
        }
        objList[rno] = nowRole;
        if (mainrole)
        {
            keyListener.main.state = state;
            keyListener.main.controler = controlList[rno];
            keyListener.main.eList = equiplist;
        }
        playerPoors.Add(new OrderPoor(rno));
    }
    public void createEnemy(int roleKind,List<int> eList, Vector2 pos,string AIname)
    {
        int rno = enemyRecondNum++;
        GameObject nowRole = Instantiate(prabTable.table[roleKind], pos, transform.rotation);
        var controler = nowRole.AddComponent<enemyControler>();
        var state = nowRole.AddComponent<enemyState>();
        nowRole.SetActive(true);
        var equiplist = nowRole.GetComponent<EquipmentList>();
        equiplist.controler = controlList[rno];
        equiplist.Start();
        hpBarCreater.CreateHpBar(nowRole, rno, "嘍啰", "你的末日");
        foreach (int index in eList)
        {
            equiplist.addByNo(index);
        }
        controler.random = new System.Random(rno);
        controler.Index = rno;
        if (AIname != null)
        {
           var ai= nowRole.AddComponent(System.Type.GetType(AIname));
            ((AI_fsyn)ai).onInit(controler);
        }
        nowRole.GetComponent<RoleState>().team = (sbyte)MAX_UNIT_NUM;
        enemyList[rno] = nowRole;

    }
	public void removeEnemy(int index)
    {
        enemyList.Remove(index);
    }
    // Update is called once per frame
	void Update () {
        bool all = true;
		foreach(OrderPoor poor in playerPoors)
        {
            if (poor.orders.Count <= 0)
            {
                all = false;
            }
        }
        //如果所有玩家的幀都收到
        if (all) {
            foreach (OrderPoor poor in playerPoors)

            {
                Debug.Log("poor length:" + poor.orders.Count);
                while (poor.orders.Count > 0)
                {
                    Dictionary<string, object> order = poor.orders[0];
                    Debug.Log("code:"+ order["code"]+"type:"+order["code"].GetType());
                    sbyte code = (sbyte)order["code"];
                    switch (code)
                    {
                        case CodeTable.ADD_BUFF:
                            {
                                controlList[poor.unitNo].realAddBuff((sbyte)order["buffNo"]);
                                break;
                            }
                        case CodeTable.CONTORTION:
                            {
                                controlList[poor.unitNo].realControtions((int)order["distortionNo"]);
                                break;
                            }
                        case CodeTable.TAKE_DAMAGE:
                            {
                                controlList[poor.unitNo].realTakeDamage(order);
                                break;
                            }
                        case CodeTable.BEEN_TREAT:
                            {
                                controlList[poor.unitNo].realBeTreat(order);
                                break;
                            }
                        case CodeTable.INTERVAL:
                            {
                                controlList[poor.unitNo].takeInterval(order);
                                break;
                            }
                        case CodeTable.KEYLEFT_DOWN:
                        case CodeTable.KEYLEFT_UP:
                        case CodeTable.KEYUP_DOWN:
                        case CodeTable.KEYUP_UP:
                        case CodeTable.KEYRIGHT_DOWN:
                        case CodeTable.KEYRIGHT_UP:
                        case CodeTable.KEYDOWN_DOWN:
                        case CodeTable.KEYDOWN_UP:
                            {
                                controlList[poor.unitNo].onMoveButtom((sbyte)code);
                                break;
                            }
                        case CodeTable.FRAME_END:
                            {
                                controlList[poor.unitNo].move(SINGLE_FRAME_TIME);
                                break;
                            }
                        case CodeTable.SET_MOUSE_POS:
                            {
                                controlList[poor.unitNo].setDirection((Vector2)order["mousePosition"]);
                                break;
                            }
                        case CodeTable.MOUSE_LEFT_DOWN: case CodeTable.MOUSE_RIGHT_DOWN: case CodeTable.KEY1_DOWN:
                        case CodeTable.KEY2_DOWN:case CodeTable.KEY3_DOWN:case CodeTable.KEY4_DOWN:
                        case CodeTable.KEY5_DOWN:
                            {
                                controlList[poor.unitNo].onSkillButtom(code, order);
                                break;
                            }
                    }
                    poor.orders.RemoveAt(0);
                }
                //poor.orders.Clear();
            }
            foreach(KeyValuePair<int,GameObject> pair in enemyList)
            {
                var econtrol = pair.Value.GetComponent<enemyControler>();
                var ai = pair.Value.GetComponent<AI_fsyn>();
                ai.onUpdate();
                Dictionary<string, object> arg = new Dictionary<string, object>();
                arg["interval"] = SINGLE_FRAME_TIME;
                econtrol.takeInterval(arg);
                econtrol.move(SINGLE_FRAME_TIME);
            }
        }
	}
}
