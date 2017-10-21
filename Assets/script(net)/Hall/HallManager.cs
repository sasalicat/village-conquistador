using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KBEngine;

public class HallManager : MonoBehaviour {
    int num;
    public static string Nickname=null;
    public Text numLabel;
    public Text NicknameLabel;
    public GameObject nameLable;
    public bool showNameLabel=false;
    public roomShow roomShowControl=null;
    public dataRegister register;
    // Use this for initialization
    void Start () {
        ((Account)KBEngine.KBEngineApp.app.player()).hallManager = this;

        register = GameObject.Find("client").GetComponent<dataRegister>();
        if (register.roleList.Count == 0)//如果Account在Hall加载之前就初始化
        {
            Debug.Log("set rolelist");
            register.roleList = ((Account)KBEngine.KBEngineApp.app.player()).lastRoles;//设置roleList
        }
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

        sbyte rolekind = register.nowRoleData.roleKind;
        List<sbyte> temp = register.nowRoleData.equipmentIdList;
        List<object> objList = new List<object>();
        for (int i = 0; i < temp.Count; i++)
        {
            objList.Add(temp[i]);
        }
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("enterRoomReq",new object[]{roomId,rolekind,objList});
        JumpRoomScene();
    }
    public void askRandomRole()
    {
        Debug.Log("ask random");
        ((Account)KBEngine.KBEngineApp.app.player()).baseCall("reRandomRole");
    }
   
}
