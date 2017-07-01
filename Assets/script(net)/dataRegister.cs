using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataRegister : MonoBehaviour {
    public class PlayerData
    {
        public int entityId;
        public RoleData role;
        public string name;
        public bool islocal;//標記是不是本機玩家
        public PlayerData(sbyte rolekind, List<sbyte> equipmentList,string name,bool islocal)
        {
            role = new RoleData(rolekind, equipmentList);
            this.name = name;
            this.islocal = islocal;
        }
    }
    public List<RoleData> roleList=new List<RoleData>();//用於存從數據庫取出的角色資料
    public PlayerData[] PlayerInWar = new PlayerData[6];//用於暫存現在有那些玩家的角色參加遊戲
    public List<sbyte> equipmentNoList;
    // Use this for initialization
	void Start () {

        roleList.Add(new RoleData(0,equipmentNoList));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
