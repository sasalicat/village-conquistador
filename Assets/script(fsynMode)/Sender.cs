using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sender : PostOffice {
    private List<Dictionary<string, object>> orders;
    public int keyState;
    public int playerNo = 0;
    protected void Start()
    {
        orders = new List<Dictionary<string, object>>();
    }
    public override void addOrder(Dictionary<string, object> order) 
    {
        orders.Add(order);
    }
    public override void updateFrame()
    {
        foreach( Dictionary<string,object> order in orders)
            fsynManager_local.main.addOrderFor(playerNo,order);
        Dictionary<string, object> inter = new Dictionary<string, object>();
        inter["code"] = CodeTable.INTERVAL;
        inter["interval"] = cycleTime;
        fsynManager_local.main.addOrderFor(playerNo, inter);
        Dictionary<string, object> end = new Dictionary<string, object>();
        end["code"] = CodeTable.FRAME_END;
        fsynManager_local.main.addOrderFor(playerNo,end);
        orders.Clear();
    }
}
