using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KBEngine;
using System;

public class movement : MonoBehaviour {
    Dictionary<Int32, float> timeRecord;
	// Use this for initialization
	void Start () {
        //KBEngine.Event.registerOut("set_position", this, "set_position");
        KBEngine.Event.registerOut("updatePosition", this, "updatePosition");
        timeRecord = new Dictionary<int, float>();
       

	}
	
	// Update is called once per frame
	void Update () {
        
	}
    public void OnUp()
    {
        KBEngineApp.app.player().cellCall("move", (byte)1);
    }
    public void OnMonst()
    {
        KBEngineApp.app.player().cellCall("createMonst");
    }
    public void updatePosition(Entity e)
    {
        /*float lastTime;
        if (timeRecord.TryGetValue(e.id,out lastTime))
        {
            Debug.Log("in update pos id：" + e.id + " 時間差:" + (Time.time - lastTime)+" 時間:"+Time.time);
        }
        timeRecord[e.id] = Time.time;*/
        ((EntityControl)e.renderObj).updatePosition(e.position);
    }
    public void set_position(Entity e)
    {
        Debug.Log("位于set_position id is：" + e.id);
    }
}
