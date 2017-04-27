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
        public void receive1(sbyte roomNo,sbyte code)
        {
            if (manager.register.PlayerInWar[roomNo].islocal)
            {
                Debug.LogError("in receive Type 1 roomNo" + roomNo + "is local player");
                return;
            }
            switch (code)
            {
                case CodeTable.KEYUP_DOWN:
                    {
                        manager.controlerList[roomNo].get_on_keyup_down()();
                        break;
                    }
                case CodeTable.KEYLEFT_DOWN:
                    {
                        manager.controlerList[roomNo].get_on_keyleft_down()();
                        break;
                    }
                case CodeTable.KEYRIGHT_DOWN:
                    {
                        manager.controlerList[roomNo].get_on_keyright_down()();
                        break;
                    }
                case CodeTable.KEYDOWN_DOWN:
                    {
                        manager.controlerList[roomNo].get_on_keydown_down()();
                        break;
                    }
                case CodeTable.KEYUP_UP:
                    {
                        manager.controlerList[roomNo].get_on_keyup_up()();
                        break;
                    }
                case CodeTable.KEYLEFT_UP:
                    {
                        manager.controlerList[roomNo].get_on_keyleft_up()();
                        break;
                    }
                case CodeTable.KEYRIGHT_UP:
                    {
                        manager.controlerList[roomNo].get_on_keyright_up()();
                        break;
                    }
                case CodeTable.KEYDOWN_UP:
                    {
                        manager.controlerList[roomNo].get_on_keydown_up()();
                        break;
                    }
            }
        }
    }
}
