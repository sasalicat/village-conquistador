using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class NetManager : MonoBehaviour ,Manager {
    public Text Label;

    public const int MAX_NUM = 6;
    public const float INTERVAL_CYCLE = 0.1f;

    private GameObject[] objList;
    //private ObjAndRoomNo[] orList;
    public NetControler[] controlerList;
    public NetPlayerControler playerContorler;
    public PrabTabel prafebTable;
    public GameObject roleparfab;
    public dataRegister register;
    public List<dirPair> directionList=new List<dirPair>();
    public bool[] finishTable = new bool[MAX_NUM];
    public int intervals = 0;//累积的时间间隔触发次数

    //public List<createOrder> createOrderList = new List<createOrder>();
    public class dirPair
    {
        public sbyte No;
        public short z;
        public dirPair(sbyte No,short z)
        {
            this.No = No;
            this.z = z;
        }
    }
    public class createOrder
    {//另一個方案,直接創建投射物,未採用
        public sbyte No;
        public Vector3 atPos;
        public Vector3 MissileRota;
        public createOrder(sbyte no,Vector3 pos,Vector3 rota)
        {
            No = no;
            atPos = pos;
            rota = MissileRota;
        }
    }
    private class ObjAndRoomNo
    {
        public sbyte roomNo;
        public GameObject obj;
        public ObjAndRoomNo(sbyte roomNo,GameObject obj)
        {
            this.roomNo = roomNo;
            this.obj = obj;
        }
    }

    public GameObject[] getGameObjectList()
    {
        return objList;
    }

    // Use this for initialization
    void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
        prafebTable = GameObject.Find("keyTabel").GetComponent<PrabTabel>();
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        //KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        objList = new GameObject[MAX_NUM];
        for(int i = 0; i < MAX_NUM; i++)
        {
            if (register.PlayerInWar[i] != null)//在這裡生成角色對應的gameobj
            {
                int roleNo=register.PlayerInWar[i].role.roleKind;
                //Debug.Log("i:"+i+" :"+roleNo);
                objList[i] =  (GameObject)Instantiate(prafebTable.table[roleNo], new Vector3(0,0,0),transform.rotation);
                

            }
        }
        //orList = new ObjAndRoomNo[MAX_NUM];
        controlerList = new NetControler[MAX_NUM];
        ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
        ((Player)KBEngineApp.app.player()).manager = this;
        ((Player)KBEngineApp.app.player()).loadPage = GameObject.Find("loadingPage").GetComponent<loading>();
        ((Player)KBEngineApp.app.player()).ms = GameObject.Find("Canvas/mslabel").GetComponent<msTask>();
        //Label = Label = Label = GameObject.Find("Canvas/Text2").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (directionList.Count > 0)
        {
            //Debug.Log(" directionList[0].z is" + directionList[0].z);
            objList[directionList[0].No].transform.eulerAngles = new Vector3(0, 0, directionList[0].z);
            directionList.RemoveAt(0);
        }
        while (intervals > 0)
        {
            //Debug.Log("in netmanager >0");
            for (int i = 0; i < MAX_NUM; i++)
            {
                if (controlerList[i] != null)
                {
                    Dictionary<string, object> newData = new Dictionary<string, object>();
                    newData["interval"] = INTERVAL_CYCLE;
                    ((KBControler)controlerList[i]).addEvent(CodeTable.INTERVAL, newData);
                }
            }
            Dictionary<string, object> newDatap = new Dictionary<string, object>();
            newDatap["interval"] = INTERVAL_CYCLE;
            ((KBControler)playerContorler).addEvent(CodeTable.INTERVAL, newDatap);
            intervals--;
        }
        /*if (createOrderList.Count > 0)//另一個方案,直接創建投射物,未採用
        {
            createOrder order = createOrderList[0];
            //Instantiate();
        }*/
    }
    private bool checkFinish()//如果所有玩家都完成加载回传true.用于本地创建
    {
        bool allFinish = true;//旗标,只要有一个玩家没有准备就会被设置为false
        for(int i = 0; i < MAX_NUM; i++)
        {
            if (register.PlayerInWar[i] != null)
            {
                if (!finishTable[i])
                {
                    allFinish = false;
                    break;
                }
            }
        }
        return allFinish;
    }
    public void onEnterWorld(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter World");
        Debug.Log("type:" + e.GetType());
    
        if (e is Player)
        {
            for (int i = 0; i < MAX_NUM; i++)
            {//roomNo就是物件雜objList的索引值
                if (e.id == register.PlayerInWar[i].entityId)
                {

                    EquipmentList elist = objList[i].GetComponent<EquipmentList>();
                    if (e.id == KBEngineApp.app.player().id)
                    {
                        NetPlayerControler control = objList[i].AddComponent<NetPlayerControler>();

                        elist.controler = control;
                        control.entity = e;


                        ((Player)e).controler = control;
                        ((Player)e).roomNo = (sbyte)i;
                        ((Player)e).manager = this;
                        control.roomNo = (sbyte)i;
                        playerContorler = control;
                        objList[i].GetComponent<NetRoleState>().control = control;
                        objList[i].GetComponent<NetRoleState>().islocal = true;


                    }
                    else
                    {

                        NetControler control = objList[i].AddComponent<NetControler>();
                        objList[i].GetComponent<EquipmentList>().controler = control;

                        elist.controler = control;
                        control.entity = e;


                        ((Player)e).controler = control;
                        ((Player)e).roomNo = (sbyte)i;
                        ((Player)e).manager = this;
                        controlerList[i] = control;
                        objList[i].GetComponent<NetRoleState>().control = control;
                        objList[i].GetComponent<NetRoleState>().islocal = false;
                    }

                    //orList[i] = new ObjAndRoomNo((sbyte)i,objList[i]); 
                    objList[i].GetComponent<NetRoleState>().roomNo = (sbyte)i;
                    objList[i].transform.position = e.position;
                    objList[i].SetActive(true);
                    finishTable[i] = true;
                    if (checkFinish())//如果本地段都全部完成则通知server本client已经完成加载
                    {
                        ((Player)KBEngineApp.app.player()).baseCall("notifyFinish", new object[] { });
                    }
                    // Label.text = "elist control is:" + elist.controler.ToString();
                    //elist.AddEquipments();
                    //Label.text = "after add";
                    break;
                }
            }
        }else if(e is Obstacle)
        {
            sbyte index = (sbyte)((Obstacle)e).getDefinedProperty("kind");
            GameObject newone = Instantiate(prafebTable.Obstacles[index],e.position,Quaternion.Euler(e.direction));
            newone.GetComponent<ObstacleState>().entity = e;
            Debug.Log("obstacle" + newone.GetComponent<ObstacleState>());
            ((Obstacle)e).state = newone.GetComponent<ObstacleState>();
        }
    }
    public void PlayerInit(Entity e)
    {
        ((Player)e).manager = this;
       
    }

    public int getMaxNum()
    {
        return MAX_NUM;
    }
    public static void createMissile(sbyte No,Vector3 pos,Vector3 rota)
    {
        ((Player)KBEngineApp.app.player()).baseCall("createMissile", new object[] { No, pos, rota });
    }
    public static void SkillTrigger(sbyte eIndex,Vector3 pos,Vector3 mousePos)
    {
        ((Player)KBEngineApp.app.player()).baseCall("notify3", new object[] { eIndex, pos, mousePos});
    }
    /*public static Vector3 getOriginalInitPoint(Vector3 oriRolePos,float angleZ,Vector3 localMissitlePos)
    {
        float newx = localMissitlePos.magnitude * Mathf.Cos(angleZ-);
    }*/
    public GameObject getObjByRoomNo(sbyte roomNo)
    {
        /*for(int i = 0; i < orList.Length; i++)
        {
            if (orList[i].roomNo == roomNo)
            {
                return orList[i].obj;
            }
        }
        return null;*/
        return objList[roomNo];
    } 
}
