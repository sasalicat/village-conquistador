namespace KBEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Player : Entity
    {
        public NetManager manager;
        public KBControler controler;
        public sbyte roomNo;
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
            //Debug.Log("update"+roomNo+":"+z);
            
            manager.directionList.Add(new NetManager.dirPair(roomNo,(short)z));
        }
        public void receive1(sbyte roomNo,sbyte code)
        {
            Debug.Log("this id is" + this.id + "player is" + KBEngineApp.app.player().id);
            Dictionary<string, object> order = new Dictionary<string, object>();
            order["code"] = code;
            controler.addOrder(order);
        }
        public void receive2(sbyte roomNo,sbyte code,Vector3 mousePos)
        {
            Debug.Log("this id is" + this.id + "player is" + KBEngineApp.app.player().id);
            Dictionary<string, object> order = new Dictionary<string, object>();
            order["code"] = code;
            order["directionZ"]= mousePos;
            controler.addOrder(order);
        }
        public void receive3(sbyte eindex,Vector3 PlayerPos,Vector3 mousePos,sbyte randint)
        {
            Dictionary<string, object> args=new Dictionary<string, object>();
            args["PlayerPosition"] = PlayerPos;
            args["MousePosition"] = mousePos;
            args["randomPoint"] = randint;
            controler.addTriggerOrder(eindex, args);
        }
        public void receive4(Vector3 selfPos, Vector3 selfEuler, Vector3 damagerPos, sbyte damagerNo,sbyte kind, short num,short stiffMilli,sbyte makeC,sbyte hitC, sbyte randomInt)
        {
            Debug.Log("makeC is" + makeC + "hitC is" + hitC);
            //damage damage = new damage(kind,num,((float)stiffMilli)/1000);
        }
    }
}
