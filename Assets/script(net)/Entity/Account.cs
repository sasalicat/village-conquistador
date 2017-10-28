namespace KBEngine
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

    public class Account:Entity
    {
        public float lastUpdateTime = 0;
        public HallManager hallManager;
        public RoomManager roomManager;
        public delegate void addRoom(string name, string num);
        public Dictionary<string, object> RoomInitData;//用于初始化房间的列表
        public List<Dictionary<string, object>> RoomChangeList=new List<Dictionary<string, object>>();
        public List<RoleData> lastRoles;//如果dataRegist没有加载则,先用这个变数存角色列表

        public static bool PlayerInRoom = false;//用于确定player的init是因为登入还是
        //public addRoom addroom_fuction;//在hallmanager裏面加 
        public override void __init__()
        {

            if (!PlayerInRoom)//登入
            {
                KBEngine.Event.fireOut("onLoginSuccessfully", new object[] { KBEngineApp.app.entity_uuid, id, this });

            }

         
        }

        /*public override void set_position(object old)
        {
            Debug.Log("on setposition");
            if (renderObj!=null) {
                base.set_position(old);
                ((EntityControl)renderObj).updatePosition(new Vector3());
            }
        }*/
        //大厅内的函数
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
                HallManager.Nickname = nickname;
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
        //房间内的函数
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
        public void setRoleList(List<object> newlist)
        {
            Debug.Log("in set_RoleList<<<<<<<<");
            List<RoleData> list = new List<RoleData>();
            Debug.Log("newlist:"+newlist+" length:"+newlist.Count);
            foreach(Dictionary<string,object> role in newlist)
            {
                Debug.Log("在转化字典中 角色编号:" + (sbyte)role["kind"]);

                List<object> elist = (List<object>)role["equipmentNos"];
                
                List<sbyte> newelist = new List<sbyte>();
                foreach(sbyte no in elist)
                {
                    newelist.Add(no);
                    Debug.Log("装备编号:" + no);
                }
                Debug.Log("role kind" + (sbyte)role["kind"]);
                list.Add(new RoleData((sbyte)role["kind"],newelist));

            }

            if (hallManager != null)
            {
                hallManager.register.roleList=list;
            } else if (roomManager!=null)
            {
                roomManager.register.roleList = list;
            }
            else//都没有加载
            {
                Debug.Log("没有加载 list Count:"+list.Count);
                lastRoles = list;
            }
        }
    }
}
