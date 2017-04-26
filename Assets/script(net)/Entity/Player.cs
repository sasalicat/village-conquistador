namespace KBEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Player : Entity
    {
        public NetManager manager;
        public override void __init__()
        {
            Debug.Log("KB: player init!!!");
            KBEngine.Event.fireOut("PlayerInit", new object[] {  this });
        }
        public void reqChangeReady(List<object> PlayerList)
        {
            Debug.Log("这是reqchangeReady的Log");
            while (PlayerList.Count > 0)
            {
                Dictionary<string, object> item = (Dictionary<string, object>)PlayerList[0];
                manager.register.PlayerInWar[(sbyte)item["roomNo"]].entityId = (int)item["eId"];
                PlayerList.RemoveAt(0);
            }
            Dbg.DEBUG_MSG("onIdReady-----------------------");
            baseCall("onIdReady", new object[] { });
        }
       public void updateZ(sbyte roomNo,short z)
        {
            Debug.Log("update"+roomNo+":"+z);
            
            manager.directionList.Add(new NetManager.dirPair(roomNo,(short)z));
        }
    }
}
