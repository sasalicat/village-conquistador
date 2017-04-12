using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class roomShow : MonoBehaviour {
    public class roomData
    {
        public string name;
        public sbyte playerNum;
        public int id;
        public roomData(int id, string name,sbyte num)
        {
            this.id = id;
            this.name = name;
            this.playerNum = num;
        }
    };
    public GameObject single;
    public GameObject Content;
    public GameObject[] roomList;
    public int[] roomLocationPoor;//用於獲得最上方的房間空位,沒有房間的項目的值會是-1,否則是該房間的roomid
    public List<roomData> handleLine = new List<roomData>();
    public dataRegister register;
    public HallManager manager;
	// Use this for initialization
	void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
        roomList = new GameObject[20];
        roomLocationPoor = new int[20];
        for(int i = 0; i < 20; i++)
        {
            roomLocationPoor[i] = -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
        while (handleLine.Count > 0)
        {
            roomData firstData= handleLine[0];
            if (roomList[firstData.id]==null) {
                Debug.Log("count is" + handleLine.Count + "0`s name is " + firstData.name);
                if(firstData.playerNum>0)
                    AddRoom(firstData.id,firstData.name, firstData.playerNum.ToString());
                
                
            }
            else
            {
                if (firstData.playerNum <= 0)//當人數為0時刪除房間
                {
                    Destroy(roomList[firstData.id]);
                    roomList[firstData.id] = null;
                    for (int i = 0; i < roomLocationPoor.Length; i++) {
                        if(roomLocationPoor[i]== firstData.id)
                        {
                            roomLocationPoor[i] = -1;
                        }
                    }

                }
                else { 
                UpdateRoom_name(firstData.id, firstData.name);
                UpdateRoom_personNum(firstData.id, firstData.playerNum.ToString());

                }
            }

            handleLine.Remove(firstData);
        }
	}
    public void OnAddClick()
    {
        object[] param = new object[1];
        param[0] = "我差不多已經是條鹹魚了";
        List<sbyte> temp = register.roleList[0].equipmentIdList;
        List<object> objList = new List<object>();
        for(int i = 0; i < temp.Count; i++)
        {
            objList.Add(temp[i]);
        }

        ((Account)(KBEngineApp.app.player())).baseCall("createRoom", new object[] { "我差不多已經是條鹹魚了", register.roleList[0].roleKind,objList});
        manager.JumpRoomScene();
    }
    public void AddRoomReq(int id,string name, sbyte num)
    {
        handleLine.Add(new roomData(id,name,num));
    }
    public void AddRoom(int id, string name,string num)
    {
        int initLocationIndex=-1;
        for(int i = 0; i < 20; i++)//用一個回圈檢索locationpoor找到第一個空位
        {
            if(roomLocationPoor[i] == -1)
            {
                initLocationIndex = i;
                roomLocationPoor[i] = id;
                break;
            }

        }
        GameObject newRoom=Instantiate(single, this.transform.position, this.transform.rotation);
        newRoom.transform.parent = Content.transform;
        newRoom.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(0,(initLocationIndex*-90)-45,0);
        newRoom.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);
        roomList[id] = newRoom;
        newRoom.GetComponent<roomItem>().roomId=id;
        newRoom.GetComponent<roomItem>().manager = manager;

        GetComponent<RectTransform>().sizeDelta = new Vector2(0,initLocationIndex * 90);
        GameObject nameObj= newRoom.transform.Find("RoomName").gameObject;
        nameObj.GetComponent<Text>().text = name;
        GameObject numObj = newRoom.transform.Find("PersonNum").gameObject;
        numObj.GetComponent<Text>().text = num;


    }
    public void UpdateRoom_name(int id,string name)
    {
        GameObject nameObj = roomList[id].transform.Find("RoomName").gameObject;
        nameObj.GetComponent<Text>().text = name;
    }
    public void UpdateRoom_personNum(int id, string num)
    {
        GameObject numObj = roomList[id].transform.Find("PersonNum").gameObject;
        numObj.GetComponent<Text>().text = num;
    }
}
