using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class netLeaveTabel : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void sureClick()
    {
        KBEngineApp.app.player().baseCall("compulsiveLeaveRoom",new object[] { });
        Application.LoadLevel("Hall");
    }
    public void cancelClick()
    {
        gameObject.SetActive(false);
    }
}
