using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour {
    public GameObject HpBar;
    public void CreateHpBar(GameObject role)
    {
       GameObject newone=Instantiate(HpBar,this.transform);
        newone.GetComponent<HpBarControler>().role = role.transform;
        newone.GetComponent<HpBarControler>().onGetRole();
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
