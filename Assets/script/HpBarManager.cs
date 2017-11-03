using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarManager : MonoBehaviour {
    public GameObject HpBar;
    private GameObject[] hpbars=new GameObject[NetManager.MAX_NUM];
    public void CreateHpBar(GameObject role, int roomNo, string name,string team)
    {
       GameObject newone=Instantiate(HpBar,this.transform);
        newone.GetComponent<HpBarControler>().role = role.transform;
        newone.GetComponent<HpBarControler>().onGetRole();
        hpbars[roomNo] = newone;
        Transform nameLabel= newone.transform.Find("名字Label");
        nameLabel.GetComponent<Text>().text=name;
        nameLabel.transform.Find("队伍Label").GetComponent<Text>().text = team;
    }

    public void deleteHpBarWith(sbyte roomNo)
    {
        Destroy(hpbars[roomNo]);
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
