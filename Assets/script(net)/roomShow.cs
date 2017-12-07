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
        public bool gaming;
        public roomData(int id, string name,sbyte num,sbyte gaming)
        {
            this.id = id;
            this.name = name;
            this.playerNum = num;
            Debug.Log("in RoomData init gamging is" + gaming);
            this.gaming = (gaming >= 0);
        }
    };
    public GameObject single;
    public GameObject Content;
    public GameObject[] roomList;
    public int[] roomLocationPoor;//用於獲得最上方的房間空位,沒有房間的項目的值會是-1,否則是該房間的roomid
    public List<roomData> handleLine = new List<roomData>();
    public dataRegister register;
    public HallManager manager;
    public GameObject createTable;


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
            Debug.Log("in room show id is" + firstData.id);
            if (roomList[firstData.id]==null) {
                Debug.Log("count is" + handleLine.Count + "0`s name is " + firstData.name);
                if(firstData.playerNum>0)
                    AddRoom(firstData.id,firstData.name, firstData.playerNum.ToString(),firstData.gaming);
                
                
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
                    UpdateRoom_gamestate(firstData.id, firstData.gaming);
                }
            }

            handleLine.Remove(firstData);
        }
	}
    public void OnAddClick()
    {
        createTable.SetActive(true);
    }
    public void OnSureClick()//用于创建房间页面的确定的listener
    {
        object[] param = new object[3];
        string roomname = createTable.transform.Find("InputField/Text").GetComponent<Text>().text;
        if (roomname != "")
        {
            param[0] = roomname;
        }
        else
        {
            param[0] = createTable.transform.Find("InputField/Placeholder").GetComponent<Text>().text;
        }
        Debug.Log("roleList " + register.roleList.Count);
        List<sbyte> temp = register.nowRoleData.equipmentIdList;
        List<object> objList = new List<object>();
        for (int i = 0; i < temp.Count; i++)
        {
            objList.Add(temp[i]);
        }
        param[1] = register.nowRoleData.roleKind;
        param[2] = objList;
        ((Account)(KBEngineApp.app.player())).baseCall("createRoom",param);
        manager.JumpRoomScene();
    }
    public void OnCancelClick()
    {
        createTable.SetActive(false);
    } 
    public void AddRoomReq(int id,string name, sbyte num,sbyte gaming)
    {
        handleLine.Add(new roomData(id,name,num,gaming));
    }
    public void EnterTraining()
    {
        Application.LoadLevel("trainingspace");
    }
    public void AddRoom(int id, string name,string num,bool gaming)
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
        Debug.Log("in add room gaming is" + gaming);
        if (gaming)
        {
            
           newRoom.GetComponent<Image>().color= new Color(1, 1, 1, 0.5f);
            
            Debug.Log("real color is"+ newRoom.GetComponent<Image>().color);  
        }
        newRoom.transform.parent = Content.transform;
        newRoom.GetComponent<RectTransform>().anchoredPosition3D=new Vector3(0,(initLocationIndex*-90)-45,0);
        newRoom.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 90);
        roomList[id] = newRoom;
        //設定item的記錄數據
        roomItem item = newRoom.GetComponent<roomItem>();
        item.roomId=id;
        item.manager = manager;
        item.num = System.Int32.Parse(num);
        item.gaming = gaming;

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
        roomList[id].GetComponent<roomItem>().num = System.Int32.Parse(num);
    }
    public void UpdateRoom_gamestate(int id,bool gaming)
    {
        
        if (gaming)
            roomList[id].GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        else
            roomList[id].GetComponent<Image>().color = Color.white;
        roomList[id].GetComponent<roomItem>().gaming = gaming;
        
    }
}
