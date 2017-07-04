using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;
using System;

public class RoomManager : MonoBehaviour {
    const int MAX_MEMBER_NUM = 6; 
    public GameObject[] ItemOtherArray = new GameObject[MAX_MEMBER_NUM];
    private Image[] headOtherArray = new Image[MAX_MEMBER_NUM];
    private Image[] readyImage = new Image[MAX_MEMBER_NUM];
    public GameObject[] ItemYouArray = new GameObject[MAX_MEMBER_NUM];
    private Image[] headYouArray = new Image[MAX_MEMBER_NUM];
    private Image[] readyButtonImage = new Image[MAX_MEMBER_NUM];

    


    public dataRegister register;
    public Account account;
    private IconStorage storage;
    private sbyte selfRoomId=-1;
    public bool change=false;
    //public Sprite Icon;
    // Use this for initialization
    void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
        account=((Account)KBEngine.KBEngineApp.app.player());
        for (int i = 0; i < MAX_MEMBER_NUM; i++)
        {
            headOtherArray[i] = ItemOtherArray[i].transform.Find("head").GetComponent<Image>();
            readyImage[i] = ItemOtherArray[i].transform.Find("ReadyOrNot").GetComponent<Image>();
            headYouArray[i] = ItemYouArray[i].transform.Find("head").GetComponent<Image>();
            readyButtonImage[i]= ItemYouArray[i].transform.Find("ReadyButton").GetComponent<Image>();
        }
        //Icon= Resources.Load("a") as Sprite;
        storage = GetComponent<IconStorage>();
        ((Account)KBEngineApp.app.player()).roomManager = this;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("player type is" + KBEngineApp.app.player().GetType());
        if (account.RoomInitData != null)//第一次进入房间时,同步资料
        {
            Debug.Log("data type is" + (account.RoomInitData).GetType());
            Debug.Log("item type is" + ((List<System.Object>)account.RoomInitData["list"])[0].GetType());
            List<System.Object> dataList = (List<System.Object>)account.RoomInitData["list"];
            selfRoomId = (sbyte)account.RoomInitData["selfRoomId"];
            Debug.Log("Yours own room id is:" + selfRoomId);


            for (int i = 0; i < dataList.Count; i++) {
                Dictionary<string, object> data = (Dictionary<string, object>)dataList[i];
                Debug.Log("type of ready is" + data["ready"].GetType());

                Debug.Log("roleRoomId" + (sbyte)data["roleRoomId"]);
                Debug.Log("roleKind" + (sbyte)data["roleKind"]);
                Debug.Log("ready" + (bool)((sbyte)data["ready"] > 0));
                Debug.Log("name" + (string)data["name"]);
                updateItem((sbyte)data["roleRoomId"], selfRoomId == (sbyte)data["roleRoomId"], (string)data["name"], (bool)((sbyte)data["ready"] > 0), (sbyte)data["roleKind"]);
                List<object> elist = (List<object>)data["equipmentList"];
                List<sbyte> newlist = new List<sbyte>();
                for(int index = 0; index < elist.Count; index++)
                {
                    newlist.Add((sbyte)elist[index]);
                }
                register.PlayerInWar[(sbyte)data["roleRoomId"]] = new dataRegister.PlayerData((sbyte)data["roleKind"],newlist,(string)data["name"],selfRoomId== (sbyte)data["roleRoomId"]);
                

            }
            account.RoomInitData = null;
        }
        while (account.RoomChangeList.Count > 0)//如果有从服务器传来的资料更新
        {
            Dictionary<string, object> dataSingle = account.RoomChangeList[0];

            Debug.Log("in while roleRoomId" + (sbyte)dataSingle["roleRoomId"]);
            Debug.Log("in while roleKind" + (sbyte)dataSingle["roleKind"]);

            object nameobj;
            if (dataSingle.TryGetValue("name", out nameobj))//有新的玩家進入房間
            {
                List<object> elist = (List<object>)dataSingle["equipmentList"];
                List<sbyte> newlist = new List<sbyte>();
                for (int index = 0; index < elist.Count; index++)
                {
                    newlist.Add((sbyte)elist[index]);
                }
                register.PlayerInWar[(sbyte)dataSingle["roleRoomId"]] = new dataRegister.PlayerData((sbyte)dataSingle["roleKind"], newlist, (string)dataSingle["name"], selfRoomId == (sbyte)dataSingle["roleRoomId"]);
                updateItem((sbyte)dataSingle["roleRoomId"], selfRoomId == (sbyte)dataSingle["roleRoomId"], (string)nameobj, (bool)((sbyte)dataSingle["ready"] > 0), (sbyte)dataSingle["roleKind"]);
                Debug.Log("add playerdata roomNo" + dataSingle["roleRoomId"] + " eList Count:" + elist.Count);
            }
            else//改變某個現有玩家的資料
            {
                if ((sbyte)dataSingle["roleKind"] < 0)
                {
                    register.PlayerInWar[(sbyte)dataSingle["roleRoomId"]] = null;
                }
                else
                {
                    List<object> elist = (List<object>)dataSingle["equipmentList"];
                    List<sbyte> newlist = new List<sbyte>();
                    for (int index = 0; index < elist.Count; index++)
                    {
                        newlist.Add((sbyte)elist[index]);
                    }
                    register.PlayerInWar[(sbyte)dataSingle["roleRoomId"]].role.roleKind = (sbyte)dataSingle["roleKind"];
                    register.PlayerInWar[(sbyte)dataSingle["roleRoomId"]].role.equipmentIdList = newlist;

                }
                updateItem((sbyte)dataSingle["roleRoomId"], selfRoomId == (sbyte)dataSingle["roleRoomId"], (bool)((sbyte)dataSingle["ready"] > 0), (sbyte)dataSingle["roleKind"]);
            }
            account.RoomChangeList.RemoveAt(0);
           
        }
        if (change)
        {
            Application.LoadLevel("warfield");
        }  
        
		
	}
    private void updateItem(int index, bool localPlayer, string name,bool ready,sbyte roleKind)
    {//這種方法適用於第一次生成.會setActive(Ture)
        Debug.Log("enter update");

        if (localPlayer)//此角色為玩家的角色
        {
            ItemYouArray[index].SetActive(true);
            ItemYouArray[index].transform.Find("name").gameObject.GetComponent<Text>().text = name;
            ItemYouArray[index].GetComponent<playerItem>().ready = ready;
            if (ready)
                readyButtonImage[index].sprite = storage.readyIcon[1];
            else
                readyButtonImage[index].sprite = storage.readyIcon[0];
            headYouArray[index].sprite = storage.headIcon[roleKind];

        }
        else
        {
            ItemOtherArray[index].SetActive(true);
            ItemOtherArray[index].transform.Find("name").gameObject.GetComponent<Text>().text = name;
            if (ready)
                readyImage[index].sprite = storage.readyIcon[1];
            else
                readyImage[index].sprite = storage.readyIcon[0];
            headOtherArray[index].sprite = storage.headIcon[roleKind];
        }
    }
    private void updateItem(int index, bool localPlayer, bool ready, sbyte roleKind)
    {//這種方法適用於更改.条件setActive(Ture),會判斷如果rolekind是-1時為離開房間
        Debug.Log("enter update2");
        if (localPlayer)//此角色為玩家的角色
        {
            if (roleKind < 0)//刪除item
            {
                ItemYouArray[index].SetActive(false);
                return;
            }
            ItemYouArray[index].GetComponent<playerItem>().ready = ready;
            if (ready)
                readyButtonImage[index].sprite = storage.readyIcon[1];
            else
                readyButtonImage[index].sprite = storage.readyIcon[0];
            headYouArray[index].sprite = storage.headIcon[roleKind];

        }
        else
        {
            if (roleKind < 0)//刪除item
            {
                ItemOtherArray[index].SetActive(false);
                return;
            }
            if (ready)
                readyImage[index].sprite = storage.readyIcon[1];
            else
                readyImage[index].sprite = storage.readyIcon[0];
            headOtherArray[index].sprite = storage.headIcon[roleKind];
        }
    }
    public void setReady(bool Ready)
    {
        ((Account)KBEngineApp.app.player()).baseCall("setReady",new object[] {selfRoomId,Ready});
    }
    public void onLeaveClick()
    {
        ((Account)KBEngineApp.app.player()).baseCall("leaveRoom", new object[] {});
        Application.LoadLevel("Hall");
    }
}
