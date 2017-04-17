using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;

public class NetManager : MonoBehaviour ,Manager {
    private GameObject[] objList;
    public GameObject roleparfab;

    public GameObject[] getGameObjectList()
    {
        return objList;
    }

    // Use this for initialization
    void Start () {
     
        KBEngine.Event.registerOut("onEnterWorld", this, "onEnterWorld");
        //KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        objList = new GameObject[6];
        ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onEnterWorld(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter World");
        GameObject newone = (GameObject)Instantiate(roleparfab, e.position, Quaternion.Euler(e.direction));
        if (e.id == KBEngineApp.app.player().id)
        {
            NetPlayerControler control = newone.AddComponent<NetPlayerControler>();
            control.entity = e;
        }
        else
        {
            NetControler control=newone.AddComponent<NetControler>();
            control.entity = e;
        }
    }
    public void onEnterSpace(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter Space");
        GameObject newone = (GameObject)Instantiate(roleparfab, e.position, Quaternion.Euler(e.direction));
        newone.AddComponent<NetPlayerControler>();
    }
    public void PlayerInit(Entity e)
    {
        ((Player)e).manager = this;
       
    }
}
