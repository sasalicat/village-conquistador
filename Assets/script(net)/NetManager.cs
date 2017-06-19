using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class NetManager : MonoBehaviour ,Manager {
    public Text Label;

    private const int MAX_NUM = 6;
    private GameObject[] objList;
    //private ObjAndRoomNo[] orList;
    public NetControler[] controlerList;
    public GameObject roleparfab;
    public dataRegister register;
    public List<dirPair> directionList=new List<dirPair>();
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
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        //KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        objList = new GameObject[MAX_NUM];
        for(int i = 0; i < MAX_NUM; i++)
        {
            if (register.PlayerInWar[i] != null)//在這裡生成角色對應的gameobj
            {
                objList[i] =  (GameObject)Instantiate(roleparfab, new Vector3(0,0,0),transform.rotation);
                

            }
        }
        //orList = new ObjAndRoomNo[MAX_NUM];
        controlerList = new NetControler[MAX_NUM];
        ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
        ((Player)KBEngineApp.app.player()).manager = this;
        Label= Label = Label = GameObject.Find("Canvas/Text2").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if (directionList.Count > 0)
        {
            //Debug.Log(" directionList[0].z is" + directionList[0].z);
            objList[directionList[0].No].transform.eulerAngles = new Vector3(0, 0, directionList[0].z);
            directionList.RemoveAt(0);
        }
        /*if (createOrderList.Count > 0)//另一個方案,直接創建投射物,未採用
        {
            createOrder order = createOrderList[0];
            //Instantiate();
        }*/
	}
    public void onEnterWorld(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter World");

        for (int i=0;i<MAX_NUM;i++) {//roomNo就是物件雜objList的索引值
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
                    objList[i].GetComponent<NetRoleState>().control = control;
                    
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
                }
                //orList[i] = new ObjAndRoomNo((sbyte)i,objList[i]); 
                objList[i].GetComponent<NetRoleState>().roomNo = (sbyte)i;
                objList[i].transform.position = e.position;
                objList[i].SetActive(true);
                Label.text = "elist control is:" + elist.controler.ToString();
                //elist.AddEquipments();
                //Label.text = "after add";
                break;
            }
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
