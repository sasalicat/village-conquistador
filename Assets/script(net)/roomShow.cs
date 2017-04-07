using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class roomShow : MonoBehaviour {
    public class roomData
    {
        public string name;
        public string playerNum;
        public int id;
        public roomData(int id, string name,string num)
        {
            this.id = id;
            this.name = name;
            this.playerNum = num;
        }
    };
    public GameObject single;
    public GameObject Content;
    public GameObject[] roomList;
    public bool[] roomLocationPoor;//用於獲得最上方的房間空位
    public List<roomData> handleLine = new List<roomData>();
    public dataRegister register;
    public HallManager manager;
	// Use this for initialization
	void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
        roomList = new GameObject[20];
        roomLocationPoor = new bool[20];
	}
	
	// Update is called once per frame
	void Update () {
        while (handleLine.Count > 0)
        {

            if (roomList[handleLine[0].id]==null) {
                Debug.Log("count is" + handleLine.Count + "0`s name is " + handleLine[0].name);
                AddRoom(handleLine[0].id,handleLine[0].name, handleLine[0].playerNum);
                
                
            }
            else
            {
                UpdateRoom_name(handleLine[0].id, handleLine[0].name);
                UpdateRoom_personNum(handleLine[0].id, handleLine[0].playerNum);
            }
            handleLine.Remove(handleLine[0]);
        }
	}
    public void OnAddClick()
    {
        object[] param = new object[1];
        param[0] = "我差不多已經是條鹹魚了";
        ((Account)(KBEngineApp.app.player())).baseCall("createRoom", new object[] { "我差不多已經是條鹹魚了", register.roleList[0].roleKind });
        manager.JumpRoomScene();
    }
    public void AddRoomReq(int id,string name, string num)
    {
        handleLine.Add(new roomData(id,name,num));
    }
    public void AddRoom(int id, string name,string num)
    {
        int initLocationIndex=-1;
        for(int i = 0; i < 20; i++)//用一個回圈檢索locationpoor找到第一個空位
        {
            if(roomLocationPoor[i] == false)
            {
                initLocationIndex = i;
                roomLocationPoor[i] = true;
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
