using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour {
    public GameObject HpBar;
    private GameObject[] hpbars=new GameObject[NetManager.MAX_NUM];
    public void CreateHpBar(GameObject role,int roomNo)
    {
       GameObject newone=Instantiate(HpBar,this.transform);
        newone.GetComponent<HpBarControler>().role = role.transform;
        newone.GetComponent<HpBarControler>().onGetRole();
        hpbars[roomNo] = newone;
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
