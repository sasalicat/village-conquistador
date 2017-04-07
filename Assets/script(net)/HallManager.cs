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
        byte rolekind = GameObject.Find("client").GetComponent<dataRegister>().roleList[0].roleKind;
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("enterRoomReq",new object[]{roomId,(sbyte)rolekind});
        JumpRoomScene();
    }
   
}
