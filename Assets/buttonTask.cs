using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using KBEngine;

public class buttonTask : MonoBehaviour {
    public InputField ID;
    public InputField PW;
    public void OnClick()
    {
        Debug.Log("Onclick");
        KBEngine.Event.fireIn("login",ID.text,PW.text,System.Text.Encoding.UTF8.GetBytes("2015.10.7"));

    }
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(transform.gameObject);
        KBEngine.Event.registerOut("onConnectStatus", this, "onConnectStatus");
        KBEngine.Event.registerOut("onLoginFailed",this,"onLoginFailed");
        KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        KBEngine.Event.registerOut("taskEvent", this, "m");
	}
	public void onConnectStatus(bool status)
    {
        if (!status)
            Debug.Log("连接错误");
        else
            Debug.Log("连接成功");
    }
    public void onLoginFailed(UInt16 s) {
        if (s == 20)
        {
            Debug.Log("登入失败"+System.Text.Encoding.ASCII.GetString(KBEngineApp.app.serverdatas()));
        }
        else
        {
            Debug.Log("登入失败2");
        }
    }
    public void onLoginSuccessfully(UInt64 uuid,Int32 id,Account account)
    {
        if (account != null)
        {
            Debug.Log("登入成功!");
            Application.LoadLevel("s2");

        }
    }
    public void m(int x)
    {
        Debug.Log("Task"+x);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
