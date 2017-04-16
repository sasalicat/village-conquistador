namespace KBEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Account:Entity
    {
        public float lastUpdateTime = 0;
        public HallManager hallManager;
        public RoomManager roomManager;
        public delegate void addRoom(string name, string num);
        public Dictionary<string, object> RoomInitData;//用于初始化房间的列表
        public List<Dictionary<string, object>> RoomChangeList;
        //public addRoom addroom_fuction;//在hallmanager裏面加 
        public override void __init__()
        {
            KBEngine.Event.fireOut("onLoginSuccessfully",new object[] { KBEngineApp.app.entity_uuid,id,this});
            RoomChangeList = new List<Dictionary<string, object>>();
        }
        /*public override void set_position(object old)
        {
            Debug.Log("on setposition");
            if (renderObj!=null) {
                base.set_position(old);
                ((EntityControl)renderObj).updatePosition(new Vector3());
            }
        }*/
        public void updateNum(int num)
        {
            if (hallManager != null)
            {
                hallManager.setNum(num);
            }
            
        }
        public void reqHallReady(string nickname)
        {
            Debug.Log("get nickname from server:"+nickname);
            if (nickname == "None")
            {
                hallManager.showNameLabel=true;
            }
            else
            {
                hallManager.Nickname = nickname;
            }
        }
        public void updateRoom(Dictionary<string,object> data)
        {
            int roomid = (int)data["roomId"];
            Debug.Log("in update room id is" + roomid);
            
                hallManager.roomShowControl.AddRoomReq((int)data["roomId"],(string)data["roomName"],((sbyte)data["playerNum"]));
                //addroom_fuction((string)data["roomName"], ((sbyte)data["playerNum"]).ToString());
            
           
        }
        public void getRoomList(Dictionary<string, object> datas)
        {
            //Debug.Log("in getRoomList list is type" + datas["list"].GetType());
            List<object> list = (List < object>) datas["list"];
            for(int i = 0; i < list.Count; i++)
            {
                Debug.Log("i="+i+"Count is"+list.Count);
                updateRoom((Dictionary<string,object>)list[i]);
            }
        }
        public void InitRoomInfo(Dictionary<string,object> info)
        {
            this.RoomInitData = info;
        }
        public void UpdateRoomInfo(Dictionary<string, object> info)
        {
            this.RoomChangeList.Add(info);
        }
        public void AddARoomInfo(Dictionary<string, object> info)//由于python不能overload的悲剧产物
        {
            this.RoomChangeList.Add(info);
        }
        public void changeToWar()
        {
            roomManager.change = true;
        }
    }
}
