using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class netLeaveTabel : MonoBehaviour {
    dataRegister register;
	// Use this for initialization
	void Start () {
        register = GameObject.Find("client").GetComponent<dataRegister>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void sureClick()
    {
        KBEngineApp.app.player().baseCall("compulsiveLeaveRoom",new object[] { });
        for(int i = 0; i < register.PlayerInWar.Length; i++)
        {
            register.PlayerInWar[i] = null;
        }
        Application.LoadLevel("Hall");
    }
    public void cancelClick()
    {
        gameObject.SetActive(false);
    }
}
