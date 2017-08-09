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
        public loading loadPage;
        public msTask ms;
        public override void __init__()
        {
            Debug.Log("KB: player init!!!");
            KBEngine.Event.fireOut("PlayerInit", new object[] {  this });
        }
        public void reqChangeReady(List<object> PlayerList)//server回应onChangeToWar(在netManager的start中呼叫) 的funtion
        {
            Debug.Log("这是reqchangeReady的Log");
            while (PlayerList.Count > 0)
            {
                Dictionary<string, object> item = (Dictionary<string, object>)PlayerList[0];
                manager.register.PlayerInWar[(sbyte)item["roomNo"]].entityId = (int)item["eId"];
                PlayerList.RemoveAt(0);
            }


            //Dbg.DEBUG_MSG("onIdReady-----------------------");
            baseCall("onIdReady", new object[] { });
        }
        public void updateZ(short z)
        {
            if (controler != null)
            {
                //Debug.Log("update"+roomNo+":"+z);
                Dictionary<string, object> order = new Dictionary<string, object>();
                order["code"] = CodeTable.UPDATE_Z;
                order["Z"] = z;
                controler.addOrder(order);
            }
        }
        public void receive1(Vector2 Position,sbyte code)
        {
            //Debug.Log("this id is" + this.id + "player is" + KBEngineApp.app.player().id);
            Dictionary<string, object> order = new Dictionary<string, object>();
            order["PlayerPosition"] = Position;
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
            Debug.Log("in r3 this id is" + this.id + "player is" + KBEngineApp.app.player().id);
            Dictionary<string, object> args=new Dictionary<string, object>();
            args["PlayerPosition"] = PlayerPos;
            args["MousePosition"] = mousePos;
            args["randomPoint"] = randint;
            controler.addTriggerOrder(eindex, args);
        }
        public void receive3_ap(sbyte eindex, Vector3 PlayerPos,sbyte TragetNo, Vector3 TragetPostion, sbyte randint)
        {
            Debug.Log("in r3_ap this id is" + this.id + "player is" + KBEngineApp.app.player().id);
            Dictionary<string, object> args = new Dictionary<string, object>();
            args["PlayerPosition"] = PlayerPos;
            args["TragetPosition"] = TragetPostion;
            args["randomPoint"] = randint;
            args["Traget"]= manager.getObjByRoomNo(TragetNo);
            controler.addTriggerOrder(eindex, args);
        }
        public void receive4(Vector3 selfPos, Vector3 selfEuler, Vector3 damagerPos, sbyte damagerNo,sbyte kind, short num,short stiffMilli,sbyte makeC,sbyte hitC, sbyte randooumInt)
        {
            
            //Debug.Log("makeC is" + makeC + "hitC is" + hitC);
            GameObject damager = manager.getObjByRoomNo(damagerNo);
            damage damage = new damage(kind, num, ((float)stiffMilli) / 1000,hitC >0 ,makeC >0 ,damager);
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["PlayerPosition"] = selfPos;
            Arg["PlayerEuler"] = selfEuler;
            Arg["DamagerPosition"] = damagerPos;
            Arg["Damager"] = damager;
            Arg["randomPoint"] = randooumInt;
            Arg["Damage"] = damage;
            controler.addEvent(CodeTable.TAKE_DAMAGE,Arg);
            //controler.addEvent(CodeTable.TAKE_DAMAGE,)
            //damage damage = new damage(kind,num,((float)stiffMilli)/1000);
        }
        public void receive5(sbyte treaterNo,short num,sbyte random)
        {
            GameObject treater = manager.getObjByRoomNo(roomNo);
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["Treater"] = treater;
            Arg["Num"] = num;
            Arg["randomPoint"] = random;
            controler.addEvent(CodeTable.BEEN_TREAT, Arg);
        }
        public void getFinish(sbyte roomNo)
        {
            loadPage.finishLine.Add(roomNo);
        }
        public void intervalTrigger()
        {
            manager.intervals++;
        }
        public void reqmsTask()
        {
            //Debug.Log("reqms");
            ms.ReqTime = System.DateTime.Now.Millisecond;
        }
        public void gameOver(sbyte winnerNo)
        {
            manager.register.winnerno = winnerNo;
            manager.overFlag = true;

        }
        public void receiveAddBuff(sbyte no)
        {
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["buffNo"] = no;
            controler.addEvent(CodeTable.ADD_BUFF, Arg);
        }
        public void receiveContortion(sbyte no)
        {
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["contortionNo"] = no;
            controler.addEvent(CodeTable.CONTORTION, Arg);
        }
        public void receiveSynchro(Vector3 pos,sbyte buttoms)
        {
            Dictionary<string, object> Arg = new Dictionary<string, object>();
            Arg["position"] = pos;
            Arg["buttomState"] = buttoms;
            controler.addEvent(CodeTable.SYNCHRO, Arg);
        }
    }
}
