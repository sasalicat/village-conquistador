using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataRegister : MonoBehaviour {
    public List<RoleData> roleList=new List<RoleData>();//用於存從數據庫取出的角色資料
   
	// Use this for initialization
	void Start () {
        roleList.Add(new RoleData(0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
