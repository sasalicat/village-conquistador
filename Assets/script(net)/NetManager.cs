using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetManager : MonoBehaviour ,Manager {
    private const int MAX_NUM = 6;
    private GameObject[] objList;
    public GameObject roleparfab;
    public dataRegister register;
    public List<dirPair> directionList=new List<dirPair>();
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
                objList[i].SetActive(false);

            }
        }
        ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
        ((Player)KBEngineApp.app.player()).manager = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (directionList.Count > 0)
        {
            Debug.Log(" directionList[0].z is" + directionList[0].z);
            objList[directionList[0].No].transform.eulerAngles = new Vector3(0, 0, directionList[0].z);
            directionList.RemoveAt(0);
        }
	}
    public void onEnterWorld(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter World");

        for (int i=0;i<MAX_NUM;i++) {
            if (e.id == register.PlayerInWar[i].entityId)
            {
                if (e.id == KBEngineApp.app.player().id)
                {
                    NetPlayerControler control = objList[i].AddComponent<NetPlayerControler>();
                    control.entity = e;
                }
                else
                {
                    NetControler control = objList[i].AddComponent<NetControler>();
                    control.entity = e;
                }
                objList[i].SetActive(true);
                break;
            }
        }
    }
    public void onEnterSpace(Entity e)//已经取消使用
    {
        Debug.Log("id " + e.id + "on Enter Space");
        GameObject newone = (GameObject)Instantiate(roleparfab, e.position, Quaternion.Euler(e.direction));
        newone.AddComponent<NetPlayerControler>();
    }
    public void PlayerInit(Entity e)
    {
        ((Player)e).manager = this;
       
    }

    public int getMaxNum()
    {
        return MAX_NUM;
    }
}
