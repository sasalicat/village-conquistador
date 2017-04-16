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
        KBEngine.Event.registerOut("onEnterSpace", this, "onEnterSpace");
        objList = new GameObject[6];
        ((Player)KBEngineApp.app.player()).baseCall("onChangeToWar", new object[] { });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onEnterWorld(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter World");
        Instantiate(roleparfab, e.position, Quaternion.Euler(e.direction));
    }
    public void onEnterSpace(Entity e)
    {
        Debug.Log("id " + e.id + "on Enter Space");
        Instantiate(roleparfab, e.position, Quaternion.Euler(e.direction));
    }
    public void PlayerInit(Entity e)
    {
        ((Player)e).manager = this;
       
    }
}
