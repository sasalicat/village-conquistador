using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class OverPage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
