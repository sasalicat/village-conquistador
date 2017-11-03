using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using UnityEngine.UI;
public class OverPage : MonoBehaviour {
    public Text VectoryLabel;//手动拉取
	// Use this for initialization
	void Start () {
        dataRegister register = GameObject.Find("client").GetComponent<dataRegister>();
        int winteam=register.winnerno;
        VectoryLabel.text = "队伍"+winteam+"胜利";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onButtomDown()
    {
        ((Account)KBEngineApp.app.player()).baseCall("ReSendRoomInfo");
        Application.LoadLevel("Room");
    }
}
