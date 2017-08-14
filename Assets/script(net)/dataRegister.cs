using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataRegister : MonoBehaviour {
    public delegate void onRoleListUpdate(List<RoleData> newList);
    public class PlayerData
    {
        public int entityId;
        public RoleData role;
        public string name;
        public bool islocal;//標記是不是本機玩家
        public sbyte team;
        public PlayerData(sbyte rolekind, List<sbyte> equipmentList,string name,bool islocal,sbyte team)
        {
            role = new RoleData(rolekind, equipmentList);
            this.name = name;
            this.islocal = islocal;
            this.team = team;
        }
    }
    private List<RoleData> rolelist = new List<RoleData>();
    public List<RoleData> roleList//用於存從數據庫取出的角色資料
    {
        get
        {
            return rolelist;
        }
        set
        {
            if (value != null)
            {
                rolelist = value;
                if (onRoleListChange != null)//通知观察者资料更新
                {
                    onRoleListChange(rolelist);
                }
                if (nowRoleIndex >= rolelist.Count)//如果选择index比角色列表还长,如果能够删除角色则可能发生
                {
                    nowRoleIndex = 0;
                }
            }
        }
       
    }
    public PlayerData[] PlayerInWar = new PlayerData[6];//用於暫存現在有那些玩家的角色參加遊戲

    public bool forDebug = false;//当这个为true的时候使用roleNo和equipmentNoList来生成角色而非roleList
    public sbyte roleNo;//用于外部修改当前角色的kind
    public List<sbyte> equipmentNoList;//用于外部修改当前角色的装备列表

    public sbyte winnerno = 0;

    private int nowRoleIndex=0;//用于记录玩家选择哪个角色

    public onRoleListUpdate onRoleListChange;
    
    public RoleData nowRoleData
    {
        get
        {
            if (forDebug)
            {
                return new RoleData(roleNo,equipmentNoList);
            }
            else
            {
                return roleList[nowRoleIndex];
            }
        }
    }
    // Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
