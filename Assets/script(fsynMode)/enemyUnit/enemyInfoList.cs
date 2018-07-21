using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyInfoList : MonoBehaviour {
    public static enemyInfoList main;
    public enemyRecord[] enemyInfos=new enemyRecord[50];
    public class enemyRecord
    {
        public int roleNo;
        public List<int> eList;
        public enemyInfo eInfo;
        public string name;
        public string teamName;
        public string AIname;
        public enemyInfo attributes;
        public int level;
        public enemyRecord(int roleNo,List<int> eList,enemyInfo eInfo,string name,string teamName,string AIname,enemyInfo attributes,int level)
        {
            this.roleNo = roleNo;
            this.eList = eList;
            this.eInfo = eInfo;
            this.name = name;
            this.teamName = teamName;
            this.AIname = AIname;
            this.attributes = attributes;
            this.level = level;
        }
    }
 	// Use this for initialization
	void OnEnable () {
        if (main == null)
        {
            main = this;
        }
        else
        {

            Destroy(this);
        }
        enemyInfos[0] = new enemyRecord(5, new List<int>() { 61 }, new enemy_lm_info(),"低配版史矛革","你的末日", "normal_warrior",new enemy_lm_info(),0);
        enemyInfos[1] = new enemyRecord(11, new List<int> { 62 }, new enemy_ls_info(), "大老鼠", "你的末日", "normal_warrior", new enemy_ls_info(), 0);
        enemyInfos[2] = new enemyRecord(2, new List<int> { 63 }, new enemy_sniper_info(), "GG手", "你的末日", "no_range_limit", new enemy_sniper_info(), 0);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
