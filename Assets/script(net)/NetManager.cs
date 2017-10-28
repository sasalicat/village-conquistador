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

    public GameObject[] objList;
    //private ObjAndRoomNo[] orList;
    public NetControler[] controlerList;
    public NetPlayerControler playerContorler;
    public PrabTabel prafebTable;
    public GameObject roleparfab;
    public dataRegister register;
    public HpBarManager hpBarCreater;//手动拉取赋值
    public cameraFollowBy follow;//手动拉取赋值
    public SkillBroad broad;//手動拉取賦值
    public List<dirPair> directionList=new List<dirPair>();
    public bool[] finishTable = new bool[MAX_NUM];
    public int intervals = 0;//累积的时间间隔触发次数
    public bool overFlag = false;//为true时执行页面切换

    private bool first = true;//第一个update的flag用于BaseCallonChangToWar

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
        Debug.Log("在netManager Start");
        register = GameObject.Find("client").GetComponent<dataRegister>();
        prafebTable = GameObject.Find("keyTabel").GetComponent<PrabTabel>();
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
        //KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        objList = new GameObject[MAX_NUM];
        for(int i = 0; i < MAX_NUM; i++)
        {
            if (register.PlayerInWar[i] != null)//在這裡生成角色對應的gameobj
            {
                int roleNo=register.PlayerInWar[i].role.roleKind;
                Debug.Log("在NetManager Start i:"+i+"创建obj种类:"+roleNo);
                objList[i] =  (GameObject)Instantiate(prafebTable.table[roleNo], new Vector3(0,0,0),transform.rotation);
                

            }
        }
        //orList = new ObjAndRoomNo[MAX_NUM];
        controlerList = new NetControler[MAX_NUM];
                //Label = Label = Label = GameObject.Find("Canvas/Text2").GetComponent<Text>();
    }
    void OnDestroy()
    {
        KBEngine.Event.deregisterOut("onEnterWorld", this, "onEnterWorld");
        KBEngine.Event.deregisterOut("onLeaveWorld", this, "onLeaveWorld");
    }
	
	// Update is called once per frame
	void Update () {
        if(first&& KBEngineApp.app.player()is Player)
        {
            ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
            ((Player)KBEngineApp.app.player()).manager = this;
            ((Player)KBEngineApp.app.player()).loadPage = GameObject.Find("loadingPage").GetComponent<loading>();
            ((Player)KBEngineApp.app.player()).ms = GameObject.Find("Canvas/mslabel").GetComponent<msTask>();
            first = false;
        }
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
        if (overFlag)
        {
            Application.LoadLevel("over");
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
            Debug.Log("玩家进场id:" +e.id);

            for (int i = 0; i < MAX_NUM; i++)
            {//roomNo就是物件objList的索引值
                Debug.Log(" i:" + i + "playerInWar: " + register.PlayerInWar[i]);
                e.renderObj = objList[i];
                if (e.id == register.PlayerInWar[i].entityId)
                {
                    Debug.Log("objList[i]"+objList[i]);
                    EquipmentList elist = objList[i].GetComponent<EquipmentList>();
                    if (e.id == KBEngineApp.app.player().id)//主角
                    {
                        NetPlayerControler control = objList[i].AddComponent<NetPlayerControler>();
                        objList[i].AddComponent<NetRoleState>();
                        elist.controler = control;
                        control.entity = e;


                        ((Player)e).controler = control;
                        ((Player)e).controler.Index = (sbyte)i;
                        ((Player)e).manager = this;
                        control.roomNo = (sbyte)i;
                        playerContorler = control;
                        objList[i].GetComponent<NetRoleState>().control = control;
                        objList[i].GetComponent<NetRoleState>().islocal = true;

                        follow.MainRole = objList[i];//使镜头跟随主角
                        broad.mainRoleElist = objList[i].GetComponent<EquipmentList>();//設置技能顯示
                        elist.on_AH_change += broad.changeAllLabel;
                    }
                    else
                    {

                        NetControler control = objList[i].AddComponent<NetControler>();
                        objList[i].AddComponent<NetRoleState>();
                        objList[i].GetComponent<EquipmentList>().controler = control;

                        elist.controler = control;
                        control.entity = e;


                        ((Player)e).controler = control;
                        ((Player)e).controler.Index = (sbyte)i;
                        ((Player)e).manager = this;
                        controlerList[i] = control;
                        objList[i].GetComponent<NetRoleState>().control = control;
                        objList[i].GetComponent<NetRoleState>().islocal = false;
                    }

                    //orList[i] = new ObjAndRoomNo((sbyte)i,objList[i]); 
                    objList[i].GetComponent<NetRoleState>().roomNo = (sbyte)i;
                    objList[i].GetComponent<NetRoleState>().team = register.PlayerInWar[i].team;
                    objList[i].transform.position = e.position;
                    objList[i].SetActive(true);
                    hpBarCreater.CreateHpBar(objList[i],i);

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

            e.renderObj = newone;
            newone.GetComponent<ObstacleState>().entity = e;
            Debug.Log("obstacle" + newone.GetComponent<ObstacleState>());
            ObstacleState obstate = newone.GetComponent<ObstacleState>();
            ((Obstacle)e).state = newone.GetComponent<ObstacleState>();
            int cno = (sbyte)e.getDefinedProperty("createrNo");
            Debug.Log("hp`s type is:"+ e.getDefinedProperty("Hp").GetType());
            obstate.maxHp = (Int16)e.getDefinedProperty("Hp");
            obstate.nowHp = (Int16)e.getDefinedProperty("Hp");
            if (cno!= -1)
            {
                
                obstate.Kind = index;

                if (cno == playerContorler.roomNo)
                {
                    obstate.Creater = playerContorler.gameObject;
                    playerContorler.gameObject.SendMessage("onCreateFinish", ((Obstacle)e).state,SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    obstate.Creater = controlerList[cno].gameObject;
                    controlerList[cno].gameObject.SendMessage("onCreateFinish", ((Obstacle)e).state, SendMessageOptions.DontRequireReceiver);
                }
            }
        }else if(e is Area)
        {
            short radiu = (short)e.getDefinedProperty("radius");
            float radiu_f = radiu / 1000;
            GameObject newArea=Instantiate(prafebTable.Areas[0], e.position, Quaternion.Euler(e.direction));
            e.renderObj = newArea;
            newArea.transform.localScale = new Vector3(radiu_f, radiu_f, 1);
            newArea.GetComponent<AreaControl>().e = e;
        }
        else if(e is Point)
        {
            sbyte kind = (sbyte)e.getDefinedProperty("kind");
            GameObject newPoint = Instantiate(prafebTable.Supplies[kind], e.position, Quaternion.Euler(e.direction));
            newPoint.GetComponent<supply>().beusedCB += ((Point)e).destoryself;
            e.renderObj = newPoint;
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
    public static void createObstacle(GameObject creater,Vector3 position,sbyte kind,short hp)
    {
        NetPlayerControler control = creater.GetComponent<NetPlayerControler>();
        if (control != null)
           control.Entity.baseCall("createObstracle", new object[] { position, kind ,hp});
    }
    public static void createObstacle(GameObject creater, Vector3 position, sbyte kind)
    {
        NetPlayerControler control = creater.GetComponent<NetPlayerControler>();
        if (control != null)
            control.Entity.baseCall("createObstracle", new object[] { position, kind,(short)1000});
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
        if (roomNo < 0)
        {
            return null;
        }
        return objList[roomNo];
    } 
    public void onLeaveWorld(Entity e)
    {
        Debug.Log("leaveRoom被呼叫");
        Destroy((GameObject)e.renderObj);
        if(e is Player)
        {
            hpBarCreater.deleteHpBarWith(((GameObject)e.renderObj).GetComponent<NetRoleState>().roomNo);
        }
    }
    public void givefollow()//用来当主角死亡时,镜头追踪其他友方角色
    {
        int teamain = playerContorler.GetComponent<NetRoleState>().team;
       for(int i=0;i<objList.Length;i++)
        {

            if (objList[i].GetComponent<NetRoleState>().team == teamain)
            {
                if (objList[i].GetComponent<KBControler>().Alive)
                {
                    follow.MainRole = objList[i];
                    break;
                }
            }
        }
    }

}
