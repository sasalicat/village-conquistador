using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class HallManager : MonoBehaviour {
    int num;
    public string Nickname=null;
    public Text numLabel;
    public Text NicknameLabel;
    public GameObject nameLable;
    public bool showNameLabel=false;
    public roomShow roomShowControl=null;
	// Use this for initialization
	void Start () {
        ((Account)KBEngine.KBEngineApp.app.player()).hallManager = this;

        //Account.addRoom a = roomShowControl.AddRoom;
        //((Account)KBEngine.KBEngineApp.app.player()).addroom_fuction = roomShowControl.AddRoom;
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("onHallReady");
        roomShowControl = transform.GetComponent<roomShow>();
        Account.PlayerInRoom=false;
     
    }
	
	// Update is called once per frame
	void Update () {
        numLabel.text = num.ToString();
        if (showNameLabel&&!nameLable.activeSelf)
        {
            nameLable.SetActive(true);
        }
        if (Nickname != null)
        {
            NicknameLabel.text = Nickname;
        }
	}
    public void setNum(int num)
    {
        this.num = num;
       
    }
    public void JumpRoomScene()
    {
        Application.LoadLevel("Room");
    }
    public void enterRoom(int roomId)
    {
        dataRegister register = GameObject.Find("client").GetComponent<dataRegister>();
        sbyte rolekind = register.roleList[0].roleKind;
        List<sbyte> temp = register.roleList[0].equipmentIdList;
        List<object> objList = new List<object>();
        for (int i = 0; i < temp.Count; i++)
        {
            objList.Add(temp[i]);
        }
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("enterRoomReq",new object[]{roomId,rolekind,objList});
        JumpRoomScene();
    }
   
}
