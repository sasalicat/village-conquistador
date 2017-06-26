using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KBEngine;
using UnityEngine.UI;
using KBEngine;


public class login : MonoBehaviour {
    public GameObject model;
    public Text inf;
	// Use this for initialization
	void Start () {
        KBEngine.Event.registerOut("onConnectState",this, "onConnectState");
        KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
        KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
        //KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        //KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        //KBEngine.Event.registerOut("onLeaveWorld", this, "onLeaveWorld");
    }
	
	// Update is called once per frame
	void Update () {
	   
	}
    public void onClick()
    {
        try
        {
            KBEngine.Event.fireIn("login", inf.text, inf.text, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
        }
        catch (Exception e)
        {

        }
     }
    public void onConnectState(bool state)
    {
        if (state)
        {
            Debug.Log("连线成功");
        }
        else
        {
            Debug.Log("连线失败");
        }
        
    }
    public void onLoginFailed(UInt64 s)
    {
        Debug.Log("登入失败");
    }
    public void onLoginSuccessfully(UInt64 uuid,Int32 id,Account account)
    {
        Debug.Log("登入成功！ uuid is" + uuid + " id is" + id);
        Application.LoadLevel("Hall");
    }
    public void onEnterWorld(Entity e)
    {
        Debug.Log("来自onEnterWorld id:"+e.id);
        GameObject newone = (GameObject)Instantiate(model, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        EntityControl newcontrl = newone.GetComponent<EntityControl>();
        newcontrl.entity = e;
        e.renderObj = newcontrl;
        if (e == KBEngineApp.app.player())
        {
            newcontrl.isPlayer = true;
        }
        Debug.Log("設置renderObj:"+e.renderObj);
    }
    public void onEnterSpace(Entity e)
    {
        Debug.Log("来自onEnterSpace id:"+e.id);
    }
    public void onLeaveWorld(Entity e)
    {
        ((EntityControl)e.renderObj).destorySelf();
    }
}
